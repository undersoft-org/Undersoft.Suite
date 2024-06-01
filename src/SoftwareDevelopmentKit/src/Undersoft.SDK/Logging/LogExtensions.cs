namespace Undersoft.SDK.Logging
{
    using Serilog.Events;
    using System.Threading.Tasks;
    using Undersoft.SDK.Uniques;

    public static partial class Log
    {
        #region Methods

        public static void Error<T>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = ex;
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Error,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),
                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Critical<T>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = ex;
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Fatal,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),
                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Info<T>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = ex;
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Information,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),
                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Failure<T>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = ex;
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Error,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),
                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Success<T>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = ex;
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Information,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Warning<T>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = ex;
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Warning,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Security<T>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = ex;
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Information,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Alert<T>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = ex;
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Fatal,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }

        public static void Error<T, TEx>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate where TEx : Exception
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = typeof(TEx).New<TEx>(message, ex);
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Error,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);

                throw t.Exception;

            });
        }
        public static void Critical<T, TEx>(this object sender, string message, object data = null, Exception ex = null)
            where T : ILogSate where TEx : Exception
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = typeof(TEx).New<TEx>(message, ex);
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Fatal,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);

                throw t.Exception;

            });
        }
        public static void Info<T, TEx>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate where TEx : Exception
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = typeof(TEx).New<TEx>(message, ex);
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Information,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Failure<T, TEx>(this object sender, string message, object data = null, Exception ex = null, bool withThrow = true) where T : ILogSate where TEx : Exception
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = typeof(TEx).New<TEx>(message, ex);
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Debug,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);

                if (withThrow)
                    throw t.Exception;
            });
        }
        public static void Success<T, TEx>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate where TEx : Exception
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = typeof(TEx).New<TEx>(message, ex);
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Debug,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Warning<T, TEx>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate where TEx : Exception
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = typeof(TEx).New<TEx>(message, ex);
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Warning,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Security<T, TEx>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate where TEx : Exception
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = typeof(TEx).New<TEx>(message, ex);
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Debug,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),

                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);
            });
        }
        public static void Alert<T, TEx>(this object sender, string message, object data = null, Exception ex = null) where T : ILogSate where TEx : Exception
        {
            Task.Run(() =>
            {
                var t = typeof(T).New<T>();
                t.Exception = typeof(TEx).New<TEx>(message, ex);
                t.DataObject = data;

                var _log = new Starlog()
                {
                    Level = LogEventLevel.Fatal,
                    Group = typeof(T).Name.ToUpper().Replace("LOG", ""),
                    Sender = sender?.GetType().FullName ?? "Undersoft.SDK.Logging",
                    State = t,
                    Message = message
                };

                logQueue.Enqueue(_log);

                throw t.Exception;
            });
        }

        #endregion
    }

    public class Starlog : IDisposable
    {
        private bool disposedValue;

        public string Message { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public string Group { get; set; }

        public string Sender { get; set; }

        public int Id { get; } = (int)((((uint)Unique.NewId) << 1) >> 1);

        public LogEventLevel Level { get; set; }

        public ILogSate State { get; set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Message = null;
                    State.DataObject = null;
                    State.Exception = null;
                    State = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public class Runlog : Baselog, ILogSate { }

    public class Datalog : Baselog, ILogSate { }

    public class Netlog : Baselog, ILogSate { }

    public class Applog : Baselog, ILogSate { }

    public class Instantlog : Baselog, ILogSate { }

    public class Eventlog : Baselog, ILogSate { }

    public class Domainlog : Baselog, ILogSate { }

    public class Infralog : Baselog, ILogSate { }

    public class Weblog : Baselog, ILogSate { }

    public class Apilog : Baselog, ILogSate { }

    public class Healthlog : Baselog, ILogSate { }

    public class Metriclog : Baselog, ILogSate { }

    public class Servicelog : Baselog, ILogSate { }

    public class Agentlog : Baselog, ILogSate { }

    public class Accesslog : Baselog, ILogSate { }

    public class Emaillog : Baselog, ILogSate { }

    public class Baselog : ILogSate
    {
        public virtual object DataObject { get; set; }

        public virtual Exception Exception { get; set; }
    }
}
