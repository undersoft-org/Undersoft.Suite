using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Sinks;
using Serilog.Settings;
using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Serilog.Settings.Configuration;
using Serilog.Expressions;

namespace Undersoft.SDK.Logging
{
    public class LogHandler : ILogHandler
    {
        private readonly ConcurrentDictionary<string, Serilog.ILogger> loggerRegistry =
            new ConcurrentDictionary<string, Serilog.ILogger>();

        private string sender;
        private ILogger logger;
        private LogEventLevel level;
        private JsonSerializerOptions jsonOptions;

        public LogHandler(JsonSerializerOptions jsonoptions, LogEventLevel level)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            string suffix = ".json";
            if (!(environment == null))
                suffix = $".{environment}.json";

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings{suffix}", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var options = new ConfigurationReaderOptions(typeof(ConsoleLoggerConfigurationExtensions).Assembly, typeof(SerilogExpression).Assembly);

            logger = Serilog.Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration, options).CreateLogger();

            jsonOptions = jsonoptions;
            this.level = level;
            sender = "Undersoft.SDK.Logging";
            loggerRegistry.TryAdd(sender, logger);
        }

        public ILogger GetLogger<TState>(TState state)
        {
            return logger.ForContext<TState>(level, typeof(TState).FullName, state);
        }

        public bool Clean(DateTime olderThen)
        {
            return true;
        }

        public bool IsEnabled(LogEventLevel logLevel)
        {
            return (((int)level - (int)logLevel) < 1);
        }

        public void Write(Starlog log)
        {
            if (IsEnabled(log.Level))
            {
                loggerChooser(log).Write(log.Level, log.State.Exception, log.Message, log.Sender, log.Group, log.Id, log.State.DataObject);
            }
        }

        public void SetLevel(LogEventLevel level)
        {
            this.level = level;
        }

        private ILogger loggerChooser(Starlog log)
        {
            if (log.Sender != sender)
            {
                sender = log.Sender;
                logger = loggerRegistry.GetOrAdd(sender, l => GetLogger(sender));
            }

            return logger;
        }

        private Starlog Optimize(Starlog log)
        {
            if (
                log.State?.DataObject != null
                && log.State.DataObject.GetType().IsAssignableTo(typeof(IEnumerable))
            )
            {
                var dataenum = ((IEnumerable)log.State.DataObject).GetEnumerator();
                dataenum.MoveNext();
                log.State.DataObject = dataenum.Current;
            }
            return log;
        }
    }
}
