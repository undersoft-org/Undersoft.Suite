using System.Collections.ObjectModel;

namespace Undersoft.SDK.Series.Complex
{
    public partial class Plot<T> : KeyedCollection<long, Place<T>>
        where T : class, IIdentifiable
    {
        private bool _directed = true;
        private bool _measured = true;
        private Metrics _metrics;

        public Plot()
        {
            _metrics = new Metrics([new Metric(MetricKind.Distance, "Click")]);
        }

        public Plot(Metrics metrics, bool directed = true, bool measured = true)
            : this(directed, measured)
        {
            _metrics = metrics;
        }

        public Plot(bool directed, bool measured)
        {
            _directed = directed;
            _measured = measured;
        }

        public Route<T> this[int indexFrom, int indexTo]
        {
            get { return this[this[indexFrom], this[indexTo]]; }
        }

        public Route<T> this[T from, T to]
        {
            get { return this[this[from], this[to]]; }
        }

        public Route<T> this[Place<T> placeFrom, Place<T> placeTo]
        {
            get
            {
                if (placeFrom.Contains(placeTo.Id))
                {
                    var routeId = $"{placeFrom.Id}{placeTo.Id}".GetHashCode();
                    Route<T> route = new Route<T>()
                    {
                        From = placeFrom,
                        To = placeTo,
                        Metrics = placeFrom.Metrics.Contains(routeId)
                            ? placeFrom.Metrics[routeId]
                            : new Metrics(_metrics, placeFrom, placeTo)
                    };
                    return route;
                }
                return null;
            }
        }

        public Place<T> this[T item]
        {
            get { return this[item.Id]; }
            set { Dictionary[item.Id] = value; }
        }

        public void AddRoute(T from, T to, Metrics set = null)
        {
            var placeFrom = this[from];
            var placeTo = this[to];
            if (set == null)
                set = new Metrics(_metrics, placeFrom, placeTo);
            else
                set = new Metrics(set, placeFrom, placeTo);
            placeFrom.Add(placeTo);

            if (_measured)
            {
                placeFrom.Metrics.Add(set);
            }
            if (!_directed)
            {
                placeTo.Add(placeFrom);
                if (_measured)
                {
                    placeTo.Metrics.Add(set);
                }
            }
        }

        public void RemoveRoute(Place<T> from, Place<T> to)
        {
            if (from.Contains(to.Id))
            {
                from.Remove(to.Id);
                from.Metrics.Remove(to.Id);
            }
        }

        public Table<Route<T>> Routes
        {
            get
            {
                Table<Route<T>> routes = new Table<Route<T>>();
                foreach (Place<T> from in this)
                {
                    foreach (Place<T> to in from)
                    {
                        var routeId = $"{from.Id}{to.Id}".GetHashCode();
                        Route<T> route = new Route<T>()
                        {
                            From = from,
                            To = to,
                            Metrics = from.Metrics.Contains(routeId)
                                ? from.Metrics[routeId]
                                : new Metrics(_metrics, from, to)
                        };
                        routes.Add(route);
                    }
                }
                return routes;
            }
        }

        public int IndexOf(T item)
        {
            int index = -1;
            index = base[item.Id].Index;
            return index;
        }

        public IList<Route<T>> GetExtremePath(
            Place<T> source,
            Place<T> target,
            MetricKind kind = MetricKind.Distance,
            ExtremeType type = ExtremeType.Minimum
        )
        {
            int[] previous = new int[Count];
            Array.Fill(previous, -1);
            double signSetFactor = 1;
            double[] neighbourValues = new double[Count];
            switch (type)
            {
                case ExtremeType.Minimum:
                    Array.Fill(neighbourValues, double.MaxValue);
                    break;
                case ExtremeType.Maximum:
                    Array.Fill(neighbourValues, 1);
                    signSetFactor = -1;
                    break;
                default:
                    Array.Fill(neighbourValues, double.MaxValue);
                    break;
            }

            neighbourValues[source.Id] = 0;
            var neighboursPriority = new PriorityQueue<Place<T>, double>();
            for (int i = 0; i < Routes.Count; i++)
            {
                neighboursPriority.Enqueue(this[i], neighbourValues[i]);
            }
            while (neighboursPriority.Count != 0)
            {
                Place<T> lowestNeighbour = neighboursPriority.Dequeue();
                for (int i = 0; i < lowestNeighbour.Count; i++)
                {
                    Place<T> lowestNeighbourNeighbour = lowestNeighbour[i];
                    double value =
                        i < lowestNeighbour.Metrics.Count
                            ? lowestNeighbour.Metrics[i][kind].Value * signSetFactor
                            : 0;
                    double total = neighbourValues[lowestNeighbour.Index] + value;
                    if (neighbourValues[lowestNeighbourNeighbour.Index] > total)
                    {
                        neighbourValues[lowestNeighbourNeighbour.Index] = total;
                        previous[lowestNeighbourNeighbour.Index] = lowestNeighbour.Index;
                        neighboursPriority.DequeueEnqueue(
                            lowestNeighbourNeighbour,
                            neighbourValues[lowestNeighbourNeighbour.Index]
                        );
                    }
                }
            }
            IList<int> indices = new List<int>();
            int index = target.Index;
            while (index >= 0)
            {
                indices.Add(index);
                index = previous[index];
            }
            indices.Reverse();
            Table<Route<T>> result = new Table<Route<T>>();
            for (int i = 0; i < indices.Count - 1; i++)
            {
                Route<T> route = this[indices[i], indices[i + 1]];
                result.Add(route);
            }
            return result;
        }

        public IList<Route<T>> GetLowestSpanning(MetricKind kind = MetricKind.Distance)
        {
            var routes = Routes.OrderBy(a => a.Metrics[kind]);
            Queue<Route<T>> queue = new Queue<Route<T>>(routes);
            Rank<T>[] ranking = new Rank<T>[Count];
            for (int i = 0; i < Count; i++)
            {
                ranking[i] = new Rank<T>() { Owner = this[i] };
            }

            List<Route<T>> result = new List<Route<T>>();
            while (result.Count < Count - 1)
            {
                Route<T> route = queue.Dequeue();
                Place<T> from = GetRoot(ranking, route.From);
                Place<T> to = GetRoot(ranking, route.To);
                if (from != to)
                {
                    result.Add(route);
                    Union(ranking, from, to);
                }
            }
            return result;
        }

        private Place<T> GetRoot(Rank<T>[] ranking, Place<T> place)
        {
            if (ranking[place.Index].Owner != place)
            {
                ranking[place.Index].Owner = GetRoot(ranking, ranking[place.Index].Owner);
            }
            return ranking[place.Index].Owner;
        }

        private void Union(Rank<T>[] ranking, Place<T> a, Place<T> b)
        {
            if (ranking[a.Index].Value > ranking[b.Index].Value)
            {
                ranking[b.Index].Owner = a;
            }
            else if (ranking[a.Index].Value < ranking[b.Index].Value)
            {
                ranking[a.Index].Owner = b;
            }
            else
            {
                ranking[b.Index].Owner = a;
                ranking[a.Index].Value++;
            }
        }

        public Table<Route<T>> GetLowestSpanning2(MetricKind kind = MetricKind.Distance)
        {
            int[] previous = new int[Count];
            previous[0] = -1;
            double[] lowestValues = new double[Count];
            Array.Fill(lowestValues, int.MaxValue);
            lowestValues[0] = 0;
            bool[] routeExist = new bool[Count];
            Array.Fill(routeExist, false);
            for (int i = 0; i < Count - 1; i++)
            {
                int lowestValueIndex = GetLowestValueIndex(lowestValues, routeExist);
                routeExist[lowestValueIndex] = true;
                for (int j = 0; j < Count; j++)
                {
                    Route<T> route = this[lowestValueIndex, j];
                    double value = route != null ? route.Metrics[kind].Value : -1;
                    if (!routeExist[j] && value > 0 && value < lowestValues[j])
                    {
                        previous[j] = lowestValueIndex;
                        lowestValues[j] = value;
                        Console.WriteLine(" --> " + route.ToString());
                    }
                }
            }
            Table<Route<T>> result = new Table<Route<T>>();
            for (int i = 1; i < Count; i++)
            {
                Route<T> route = this[previous[i], i];
                result.Add(route);
            }
            return result;
        }

        private int GetLowestValueIndex(double[] values, bool[] routeExist)
        {
            double minValue = double.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < Count; i++)
            {
                if (!routeExist[i] && values[i] < minValue)
                {
                    minValue = values[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }

        public int[] Color()
        {
            int[] colors = new int[Count];
            Array.Fill(colors, -1);
            colors[0] = 0;
            bool[] availability = new bool[Count];
            for (int i = 1; i < Count; i++)
            {
                Array.Fill(availability, true);
                int colorIndex = 0;
                foreach (Place<T> neighbor in this[i])
                {
                    colorIndex = colors[neighbor.Index];
                    if (colorIndex >= 0)
                    {
                        availability[colorIndex] = false;
                    }
                }
                colorIndex = 0;
                for (int j = 0; j < availability.Length; j++)
                {
                    if (availability[j])
                    {
                        colorIndex = j;
                        break;
                    }
                }
                colors[i] = colorIndex;
            }
            return colors;
        }

        protected override long GetKeyForItem(Place<T> item)
        {
            return item.Id == 0 ? DateTime.UtcNow.Ticks.ToString().GetHashCode() : item.Id;
        }

        protected override void InsertItem(int index, Place<T> item)
        {
            item.Index = index;
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            for (int i = index + 1; i < Count; i++)
            {
                this[i].Index = i - 1;
            }
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, Place<T> item)
        {
            item.Index = index;
            base.SetItem(index, item);
        }
    }
}
