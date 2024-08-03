using System.Collections.ObjectModel;

namespace Undersoft.SDK.Series.Complex
{
    public class Metrics : KeyedCollection<MetricKind, Metric>, IIdentifiable
    {
        public Metrics() { }

        public Metrics(IEnumerable<Metric> metrics)
        {
            foreach (var metric in metrics)
                this.Add(metric);
        }

        public Metrics(IIdentifiable from, IIdentifiable to)
        {
            Add(new Metric(MetricKind.Distance, "Click"));
            Id = $"{from.Id}{to.Id}".GetHashCode();
            TypeId = from.TypeId;
        }

        public Metrics(IEnumerable<Metric> metrics, IIdentifiable from, IIdentifiable to)
            : this(from, to)
        {
            foreach (var metric in metrics)
                this.Add(metric);
        }

        public long Id { get; set; }
        public long TypeId { get; set; }

        protected override MetricKind GetKeyForItem(Metric item)
        {
            return item.Kind;
        }
    }
}
