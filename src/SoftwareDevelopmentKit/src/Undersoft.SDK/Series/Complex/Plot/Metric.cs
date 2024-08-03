namespace Undersoft.SDK.Series.Complex
{
    public struct Metric
    {
        public Metric(Metric metric)
        {
            Kind = metric.Kind;
            Value = metric.Value;
            Unit = metric.Unit;
        }

        public Metric(MetricKind kind, string unit, double value = 0)
        {
            Kind = kind;
            Value = value;
            Unit = unit;
        }

        public MetricKind Kind { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
    }
}
