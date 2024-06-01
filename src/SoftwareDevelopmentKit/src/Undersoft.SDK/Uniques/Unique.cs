namespace Undersoft.SDK.Uniques
{
    using System.Collections.Concurrent;
    using System.Threading;
    using Hashing;

    public static class Unique
    {
        private static readonly int CAPACITY = 75 * 1000;
        private static readonly int LOW_LIMIT = 50 * 1000;
        private static readonly int NEXT_KEY_VECTOR = (int)UniquePrimes.Get(4);
        private static readonly int WAIT_LOOPS = 500;
        private static UniqueKey32 bit32 = new UniqueKey32();
        private static UniqueKey64 bit64 = new UniqueKey64();
        private static object holder = new object();
        private static long keyNumber = (long)DateTime.Now.Ticks;
        private static ConcurrentQueue<long> keys;
        private static Random randomSeed = new Random((int)(DateTime.Now.Ticks.UniqueKey32()));

        static Unique()
        {
            keys = new ConcurrentQueue<long>();
            generate();
        }

        public static UniqueKey32 Bit32 => bit32;

        public static UniqueKey64 Bit64 => bit64;  

        public static long NewId
        {
            get
            {
                long key = 0;
                int counter = 0;
                bool loop = false;
                while (counter < WAIT_LOOPS)
                {
                    if (!(loop = keys.TryDequeue(out key)))
                    {
                        counter++;
                    }
                    else
                    {
                        if (keys.Count < LOW_LIMIT)
                            generate();
                        break;
                    }
                }
                return key;
            }
        }

        private unsafe static void keyGeneration()
        {
            lock (holder)
            {
                long seed = nextSeed();
                int count = CAPACITY - keys.Count;
                for (int i = 0; i < count; i++)
                {
                    long keyNo = nextKeyNumber();
                    keys.Enqueue((long)Hasher64.ComputeKey(((byte*)&keyNo), 8, seed));
                }
            }
        }

        private static unsafe long nextKeyNumber()
        {
            return keyNumber += NEXT_KEY_VECTOR;
        }

        private static long nextSeed()
        {
            return randomSeed.Next();
        }

        private static void generate()
        {
            Task.Run(keyGeneration);
        }
    }
}
