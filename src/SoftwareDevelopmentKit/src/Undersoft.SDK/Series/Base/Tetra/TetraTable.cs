namespace Undersoft.SDK.Series.Base.Tetra
{
    public struct TetraTable<V> : IDisposable
    {
        public TetraTable(TetraSeriesBase<V> hashcatalog, int size = 8)
        {
            EvenPositiveSize = hashcatalog.EmptyItemTable(size);
            OddPositiveSize = hashcatalog.EmptyItemTable(size);
            EvenNegativeSize = hashcatalog.EmptyItemTable(size);
            OddNegativeSize = hashcatalog.EmptyItemTable(size);
            tetraTable = new ISeriesItem<V>[4][]
            {
                EvenPositiveSize,
                OddPositiveSize,
                EvenNegativeSize,
                OddNegativeSize
            };
        }

        public unsafe ISeriesItem<V>[] this[uint id]
        {
            get { return tetraTable[id]; }
            set { tetraTable[id] = value; }
        }

        public unsafe ISeriesItem<V> this[uint id, uint pos]
        {
            get { return this[id][pos]; }
            set { this[id][pos] = value; }
        }

        public unsafe ISeriesItem<V>[] this[ulong key]
        {
            get { return this[(uint)((key & 1) | ((key >> 62) & 2))]; }
            set { this[(uint)((key & 1) | ((key >> 62) & 2))] = value; }
        }

        public unsafe ISeriesItem<V> this[ulong key, long size]
        {
            get { return this[(uint)((key & 1) | ((key >> 62) & 2))][(int)(key % (uint)size)]; }
            set { this[(uint)((key & 1) | ((key >> 62) & 2))][(int)(key % (uint)size)] = value; }
        }

        public static int GetId(ulong key)
        {
            ulong ukey = (ulong)key;
            return (int)((ukey & 1) | ((ukey >> 62) & 2));
        }

        public static int GetPosition(ulong key, long size)
        {
            return (int)(key % (ulong)size);
        }

        private ISeriesItem<V>[] EvenPositiveSize;
        private ISeriesItem<V>[] OddPositiveSize;
        private ISeriesItem<V>[] EvenNegativeSize;
        private ISeriesItem<V>[] OddNegativeSize;

        private ISeriesItem<V>[][] tetraTable;

        public void Dispose()
        {
            EvenPositiveSize = null;
            OddPositiveSize = null;
            EvenNegativeSize = null;
            OddNegativeSize = null;
            tetraTable = null;
        }
    }
}
