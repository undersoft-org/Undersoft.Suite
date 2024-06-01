using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Metrics;
using System.Diagnostics;

namespace Undersoft.SDK.Service
{
    public class Instrumentation : IDisposable
    {
        internal const string ActivitySourceName = "Examples.AspNetCore";
        internal const string MeterName = "Examples.AspNetCore";
        private readonly Meter meter;

        public Instrumentation()
        {
            string? version = typeof(Instrumentation).Assembly.GetName().Version?.ToString();
            this.ActivitySource = new ActivitySource(ActivitySourceName, version);
            this.meter = new Meter(MeterName, version);
            this.RequestCounter = this.meter.CreateCounter<long>("Request counter", "The number of requests");
        }

        public ActivitySource ActivitySource { get; }

        public Counter<long> RequestCounter { get; }

        public void Dispose()
        {
            this.ActivitySource.Dispose();
            this.meter.Dispose();
        }
    }
}