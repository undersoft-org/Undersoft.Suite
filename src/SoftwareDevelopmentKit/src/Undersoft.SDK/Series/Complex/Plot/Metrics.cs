using System.Collections.ObjectModel;

namespace Undersoft.SDK.Series.Complex
{
    public class Metrics : KeyedCollection<MetricKind, Metric>, IIdentifiable
    {
        public Metrics() { }

        public Metrics(IEnumerable<Metric> metrics)
        {
            if (metrics != null)
                foreach (var metric in metrics)
                    this.Add(metric);
        }

        public Metrics(IIdentifiable from, IIdentifiable to) : this([new Metric(MetricKind.Distance, "Click")])
        {
            Id = $"{from.Id}{to.Id}".GetHashCode();
            TypeId = from.TypeId;
        }

        public Metrics(IEnumerable<Metric> metrics, IIdentifiable from, IIdentifiable to) : this(metrics)
        {
            Id = $"{from.Id}{to.Id}".GetHashCode();
            TypeId = from.TypeId;
        }

        public long Id { get; set; }
        public long TypeId { get; set; }

        protected override MetricKind GetKeyForItem(Metric item)
        {
            return item.Kind;
        }
    }
}
