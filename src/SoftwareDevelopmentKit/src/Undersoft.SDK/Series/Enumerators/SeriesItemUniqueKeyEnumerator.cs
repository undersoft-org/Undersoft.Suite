namespace Undersoft.SDK.Series
{
    using System.Collections;
    using System.Collections.Generic;

    public class SeriesItemUniqueKeyEnumerator<V> : IEnumerator<long>, IEnumerator
    {
        public ISeriesItem<V> Entry;
        private ISeries<V> map;

        public SeriesItemUniqueKeyEnumerator(ISeries<V> Map)
        {
            map = Map;
            Entry = map.First;
        }

        public object Current => Entry.Id;

        public long Key
        {
            get { return Entry.Id; }
        }

        public V Value
        {
            get { return Entry.Value; }
        }

        long IEnumerator<long>.Current => Entry.Id;

        public void Dispose()
        {
            Entry = map.First;
        }

        public bool MoveNext()
        {
            Entry = Entry.Next;
            if (Entry != null)
            {
                if (Entry.Removed)
                    return MoveNext();
                return true;
            }
            return false;
        }

        public void Reset()
        {
            Entry = map.First;
        }
    }
}
