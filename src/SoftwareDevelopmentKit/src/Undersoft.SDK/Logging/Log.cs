namespace Undersoft.SDK.Logging
{
    using Serilog.Events;
    using System.Collections.Concurrent;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading;

    public static partial class Log
    {
        private static readonly int BACK_LOG_DAYS = -1;
        private static readonly int BACK_LOG_HOURS = -1;
        private static readonly int BACK_LOG_MINUTES = -1;
        private static readonly int SYNC_CLOCK_INTERVAL = 15;
        private static readonly JsonSerializerOptions jsonOptions;
        private static Task loggingTask;
        private static CancellationTokenSource cancellation = new CancellationTokenSource();
        private static int _logLevel = 2;
        private static bool cleaningEnabled = false;
        private static DateTime clearLogTime;
        private static ILogHandler handler { get; set; }

        private static ConcurrentQueue<Starlog> logQueue = new ConcurrentQueue<Starlog>();

        private static bool threadLive;

        public static DateTime Clock = DateTime.Now;

        static Log()
        {
            jsonOptions = JsonOptionsBuilder();
            handler = new LogHandler(jsonOptions, LogEventLevel.Information);
            clearLogTime = DateTime.Now
                .AddDays(BACK_LOG_DAYS)
                .AddHours(BACK_LOG_HOURS)
                .AddMinutes(BACK_LOG_MINUTES);
            Start(_logLevel);
        }

        public static void Add(LogEventLevel logLevel, string category, string message, ILogSate state)
        {
            var _log = new Starlog()
            {
                Level = logLevel,
                Sender = category,
                State = state,
                Message = message
            };

            logQueue.Enqueue(_log);
        }

        public static void ClearLog()
        {
            if (!cleaningEnabled || handler == null)
                return;

            try
            {
                if (DateTime.Now.Day != clearLogTime.Day)
                {
                    if (DateTime.Now.Hour != clearLogTime.Hour)
                    {
                        if (DateTime.Now.Minute != clearLogTime.Minute)
                        {
                            handler.Clean(clearLogTime);
                            clearLogTime = DateTime.Now;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreateHandler(LogEventLevel level)
        {
            handler = new LogHandler(jsonOptions, level);
        }

        public static void SetLevel(int logLevel)
        {
            _logLevel = logLevel;
        }

        public static void Start(int logLevel)
        {
            CreateHandler(LogEventLevel.Information);
            SetLevel(logLevel);
            if (!threadLive)
            {
                threadLive = true;
                loggingTask = Task.Run(logging, cancellation.Token);
            }
        }

        private static async void logging()
        {
            try
            {
                int syncInterval = SYNC_CLOCK_INTERVAL;
                while (threadLive)
                {
                    if (--syncInterval > 0)
                        Clock = Clock.AddMilliseconds(1005);
                    else
                    {
                        Clock = DateTime.UtcNow;
                        syncInterval = SYNC_CLOCK_INTERVAL;
                    }
                    await Task.Delay(1000);
                    if (handler != null)
                    {
                        int count = logQueue.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (logQueue.TryDequeue(out Starlog log))
                            {
                                handler.Write(log);
                            }
                        }
                    }

                    if (cleaningEnabled)
                        ClearLog();
                }
            }
            catch (Exception ex)
            {
                Stop();
                throw ex;
            }
        }

        private static void Stop()
        {
            cancellation.Cancel();
            threadLive = false;
        }

        private static JsonSerializerOptions JsonOptionsBuilder()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new LogExceptionConverter());
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.DefaultIgnoreCondition = System.Text.Json
                .Serialization
                .JsonIgnoreCondition
                .WhenWritingNull;
            options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            return options;
        }
    }
}
