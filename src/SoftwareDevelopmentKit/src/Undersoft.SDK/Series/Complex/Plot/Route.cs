namespace Undersoft.SDK.Series.Complex
{
    public class Route<T> : IIdentifiable
        where T : IIdentifiable
    {
        public Place<T> From { get; set; }
        public Place<T> To { get; set; }
        public Metrics Metrics { get; set; }

        public long Id
        {
            get => $"{From.Id}{To.Id}".GetHashCode();
            set => throw new NotSupportedException();
        }
        public long TypeId
        {
            get => From.TypeId;
            set => throw new NotSupportedException();
        }

        public override string ToString()
        {
            return $"Route: {From.Value.Id} -> {To.Value.Id}, weight: {Metrics[0].Value}";
        }
    }
}
