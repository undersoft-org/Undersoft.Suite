namespace Undersoft.SDK.Series.Complex
{
    public class Metric
    {
        public Metric() { }

        public Metric(Metric metric)
        {
            Kind = metric.Kind;
            Value = metric.Value;
            Unit = metric.Unit;
            Ranges = metric.Ranges;
        }

        public Metric(MetricKind kind, string unit, params MetricRange[] ranges)
        {
            Kind = kind;
            Unit = unit;
            if (!ranges.Any())
                ranges = [new MetricRange()];
            Ranges = ranges;
        }

        public Metric(MetricKind kind, string unit, double value)
        {
            Kind = kind;
            Unit = unit;
            Value = value;
        }

        public MetricKind Kind { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public MetricRange[] Ranges { get; set; }
    }
}
