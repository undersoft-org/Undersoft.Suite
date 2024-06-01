namespace Undersoft.SDK.Series.Base.Tetra
{
    public struct TetraCount
    {
        public unsafe int this[uint id]
        {
            get
            {
                fixed (TetraCount* a = &this)
                    return *&((int*)a)[id];
            }
            set
            {
                fixed (TetraCount* a = &this)
                    *&((int*)a)[id] = value;
            }
        }

        public unsafe int Increment(uint id)
        {
            fixed (TetraCount* a = &this)
                return ++(*&((int*)a)[id]);
        }

        public unsafe int Decrement(uint id)
        {
            fixed (TetraCount* a = &this)
                return --(*&((int*)a)[id]);
        }

        public unsafe void Reset(uint id)
        {
            fixed (TetraCount* a = &this)
            {
                (*&((int*)a)[id]) = 0;
            }
        }

        public unsafe void ResetAll()
        {
            fixed (TetraCount* a = &this)
            {
                (*&((long*)a)[0]) = 0L;
                (*&((long*)a)[1]) = 0L;
            }
        }

        public int EvenPositiveCount;
        public int OddPositiveCount;
        public int EvenNegativeCount;
        public int OddNegativeCount;
    }
}
