using System.Collections.ObjectModel;

namespace Undersoft.SDK.Series.Complex
{
    public partial class Plot<T> : KeyedCollection<long, Place<T>>
        where T : class, IIdentifiable
    {
        private bool _directed = true;
        private bool _measured = true;
        private Metrics _metrics = new Metrics([new Metric(MetricKind.Time, "Seconds")]);

        public Plot()
        {
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
            get
            {
                return this[((IList<Place<T>>)this)[indexFrom], ((IList<Place<T>>)this)[indexTo]];
            }
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
            AddRoute(placeFrom, placeTo, set);
        }
        public void AddRoute(Place<T> from, Place<T> to, Metrics set = null)
        {
            var placeFrom = from;
            var placeTo = to;
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

        public IList<Route<T>> QuickPath(
            Place<T> source,
            Place<T> target,
            MetricKind kind = MetricKind.Time,
            params MetricRange[] ranges
        )
        {
            if (ranges.Any())
                _metrics[kind].Ranges = ranges;
            else
                ranges = _metrics[kind].Ranges;

            int[] previous = new int[Count];
            Array.Fill(previous, -1);
            double[] neighborValues = new double[Count];
            Array.Fill(neighborValues, double.MaxValue);
            neighborValues[source.Index] = 0;
            
            var neighborsPriority = new PriorityQueue<Place<T>, double>();        
            neighborsPriority.Enqueue(((IList<Place<T>>)this)[source.Index], 0);            

            while (neighborsPriority.TryDequeue(out var lowestNeighbor, out var priority))
            {
                for (int i = 0; i < lowestNeighbor.Count; i++)
                {
                    int lowestNeighborIndex = lowestNeighbor.Index;
                    double value = ((IList<Metrics>)lowestNeighbor.Metrics)[i][kind].Value;
                    bool inRange = false;
                    foreach(var range in ranges)  
                        if (range.Minimum <= value && range.Maximum >= value)
                        {
                            inRange = true;
                            break;
                        }             
                    
                    if (inRange)
                    {
                        double total = neighborValues[lowestNeighborIndex] + value;
                        Place<T> lowestNeighborNeighbor = ((IList<Place<T>>)lowestNeighbor)[i];
                        int lowestNeighborNeughborIndex = lowestNeighborNeighbor.Index;
                        if (neighborValues[lowestNeighborNeughborIndex] > total)
                        {
                            neighborValues[lowestNeighborNeughborIndex] = total;
                            previous[lowestNeighborNeughborIndex] = lowestNeighborIndex;
                            neighborsPriority.Enqueue(lowestNeighborNeighbor, total);
                        }
                    }
                }
            }
           
            IList<int> indices = new List<int>();
            int index = target.Index;
            while (index > -1)
            {
                indices.Add(index);
                index = previous[index];
            }

            indices = indices.Reverse().ToArray();
            Table<Route<T>> result = new Table<Route<T>>();
            for (int i = 0; i < indices.Count - 1; i++)
            {
                Route<T> route = this[indices[i], indices[i + 1]];
                result.Add(route);
            }
            return result;
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
