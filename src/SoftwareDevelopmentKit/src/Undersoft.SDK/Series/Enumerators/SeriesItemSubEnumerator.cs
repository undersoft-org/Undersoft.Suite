namespace Undersoft.SDK.Series.Enumerators
{
    using System.Collections;
    using System.Collections.Generic;

    public class SeriesItemSubEnumerator<V> : IEnumerator<ISeriesItem<V>>, IEnumerator<V>, IEnumerator
    {
        public ISeriesItem<V> Entry;
        private ISeriesItem<V> map;

        public SeriesItemSubEnumerator(ISeriesItem<V> map)
        {
            this.map = map;
            Entry = map;
        }

        public object Current => Entry.Value;

        public int Index => Entry.Index;

        public long Key => Entry.Id;

        public V Value => Entry.Value;

        ISeriesItem<V> IEnumerator<ISeriesItem<V>>.Current => Entry;

        V IEnumerator<V>.Current => Entry.Value;

        public void Dispose()
        {
            Entry = map;
        }

        public bool MoveNext()
        {
            Entry = map.MoveNext(Entry);
            if (Entry != null)
            {
                return true;
            }
            return false;
        }

        public void Reset()
        {
            Entry = map;
        }
    }
}
