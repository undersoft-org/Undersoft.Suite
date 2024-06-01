namespace System.Series.Tests
{
    using Undersoft.SDK.Uniques;
    using System;
    using System.Collections.Generic;
    using Undersoft.SDK.Tests.Instant;

    public static class PrepareTestListings
    {
        public static IList<Agreement> prepareIdentifiableObjectTestCollection()
        {
            List<Agreement> list = new List<Agreement>();
            string now = DateTime.Now.ToString() + "_prepareStringKeyTestCollection";
            long max = uint.MaxValue + 250000L;
            for (long i = uint.MaxValue; i < max; i++)
            {
                string str = i.ToString() + "_" + now;
                var obj = new Agreement() { Id = str.UniqueKey(), Label = str };
                list.Add(obj);
            }
            return list;
        }


        public static IList<KeyValuePair<object, string>> prepareIdentifierKeyTestCollection()
        {
            List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>>();
            string now = DateTime.Now.ToString() + "_prepareStringKeyTestCollection";
            long max = uint.MaxValue + 250000L;
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
            for (int i = 0; i < 250000; i++)
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
            long max = uint.MaxValue + 250000L;
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
            for (int i = 0; i < 250000; i++)
            {
                long date = DateTime.Now.ToBinary();
                list.Add(
                    new KeyValuePair<object, string>(
                        (i + 1000000000).ToString() + now + new Usid(date).ToString(),
                        i.ToString() + "_" + now
                    )
                );
            }
            List<object> keys = new List<object>();
            now = "_prepareObjectKeyTestCollection";
            for (int i = 0; i < 250000; i++)
            {
                keys.Add(
                    (i + 1000).ToString() + now + new Usid(DateTime.Now.ToBinary()).ToString()
                );
            }
            List<long> hashes = new List<long>();
            foreach (var s in keys)
            {
                hashes.Add(s.UniqueKey64());
            }
            return list;
        }

        public static IList<KeyValuePair<object, string>> prepareObjectKeyTestCollection()
        {
            List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>>();
            string now = DateTime.Now.ToString() + "_prepareStringKeyTestCollection";
            for (int i = 0; i < 250000; i++)
            {
                string str = i.ToString() + "_" + now;
                list.Add(
                    new KeyValuePair<object, string>(
                        new object[]
                        {
                            (i + 1000).ToString(),
                            now,
                            new Usid(DateTime.Now.ToBinary())
                        },
                        str
                    )
                );
            }
            List<object[]> keys = new List<object[]>();
            now = "_prepareObjectKeyTestCollection";
            for (int i = 0; i < 250000; i++)
            {
                keys.Add(
                    new object[]
                    {
                        (i + 1000).ToString(),
                        now,
                        new Usid(DateTime.Now.ToBinary()).ToString()
                    }
                );
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
