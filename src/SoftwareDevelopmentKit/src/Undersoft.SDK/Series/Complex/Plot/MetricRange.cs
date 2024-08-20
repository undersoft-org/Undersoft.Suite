namespace Undersoft.SDK.Series.Complex
{
    public record MetricRange
    {
        public double Minimum { get; init; }
        public double Maximum { get; init; }

        public MetricRange(double min = 0, double max = double.MaxValue)
        {
            Minimum = min;
            Maximum = max;
        }

        public MetricRange(double value)
        {
            Minimum = value;
            Maximum = value;   
        }
    }
}
