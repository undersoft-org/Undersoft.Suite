namespace System.Series.Tests
{
    using System;
    using System.Collections.Generic;
    using Undersoft.SDK.Uniques;

    public static class PrepareTestListings
    {
        public static IList<KeyValuePair<object, string>> prepareIdentifierKeyTestCollection()
        {
            List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>>();
            string now = DateTime.Now.ToString() + "_prepareStringKeyTestCollection";
            long max = uint.MaxValue + 2000 * 100L;
            for (long i = uint.MaxValue; i < max; i++)
            {
                string str = i.ToString() + "_" + now;
                list.Add(new KeyValuePair<object, string>(new Uscn(i), str));
            }
            return list;
        }

        public static IList<KeyValuePair<object, string>> prepareIntKeyTestCollection()
        {
            List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>>();
            string now = DateTime.Now.ToString() + "_prepareStringKeyTestCollection";
            for (int i = 0; i < 2000 * 100; i++)
            {
                string str = i.ToString() + "_" + now;
                list.Add(new KeyValuePair<object, string>(i, str));
            }
            return list;
        }

        public static IList<KeyValuePair<object, string>> prepareLongKeyTestCollection()
        {
            List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>>();
            string now = DateTime.Now.ToString() + "_prepareStringKeyTestCollection";
            long max = uint.MaxValue + (2000 * 100L);
            for (long i = uint.MaxValue; i < max; i++)
            {
                string str = i.ToString() + "_" + now;
                list.Add(new KeyValuePair<object, string>(i, str));
            }
            return list;
        }

        public static IList<KeyValuePair<object, string>> prepareStringKeyTestCollection()
        {
            List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>>();
            string now = DateTime.Now.ToString() + "_prepareStringKeyTestCollection";
            for (int i = 0; i < 2000 * 100; i++)
            {
                string str = i.ToString() + "_" + now;
                list.Add(
                    new KeyValuePair<object, string>(
                        (i + 1000).ToString() + Unique.NewId.ToString(),
                        str
                    )
                );
            }
            List<object> keys = new List<object>();
            for (int i = 0; i < 2000 * 100; i++)
            {
                keys.Add(list[i].Key);
            }
            List<long> hashes = new List<long>();
            foreach (var s in keys)
            {
                hashes.Add(s.UniqueKey64());
            }
            return list;
        }
    }
}
