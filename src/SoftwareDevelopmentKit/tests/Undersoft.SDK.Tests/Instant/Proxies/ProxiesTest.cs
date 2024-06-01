using Undersoft.SDK.Tests.Instant;

namespace Undersoft.SDK.Tests.Proxies
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Reflection;
    using Undersoft.SDK.Instant;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Proxies;
    using Undersoft.SDK.Uniques;

    [TestClass]
    public class ProxiesTest
    {
        private IInstant? Instant;
        private IInstantSeries? Proxies;
        private ProxySeriesCreator? SeriesCreator;

        [TestMethod]
        public void Proxies_SelectionFromInstantSeriesMultiNesting_Test()
        {
            SeriesCreator = new ProxySeriesCreator<FieldsAndPropertiesModel>("InstantSequence_Compilation_Test");
            Proxies = SeriesCreator.Create();

            Instant = Proxy_Compilation_Helper_Test(Proxies, new FieldsAndPropertiesModel());

            long idSeed = (long)Instant["Id"];
            DateTime now = DateTime.Now;
            for (int i = 0; i < 250000; i++)
            {
                IProxy _proxy = Proxies.NewProxy();
                _proxy.Target = Proxies.NewProxy(); ;
                _proxy.Id = (long)(idSeed + i);
                _proxy["Time"] = now;
                Proxies.Add(_proxy);
            }

            long[] keyarray = new long[60 * 1000];
            for (int i = 0; i < 60000; i++)
            {
                keyarray[i] = Unique.NewId;
            }

            Proxies.Add(Proxies.NewProxy());
            Proxies[0, 4] = Instant[4];

            IInstantSeries isel1 = new ProxySeriesCreator(Proxies, true).Create();
            IInstantSeries isel2 = new ProxySeriesCreator(isel1).Create();

            foreach (var card in Proxies)
                isel2.Add(card);

            Instant = Proxy_Compilation_Helper_Test(Proxies, new FieldsAndPropertiesModel());

            isel2.Add(Instant);
            isel2[0, 4] = Instant[4];

            Assert.AreEqual(Instant[4], isel2[0, 4]);
            Assert.AreEqual(Proxies.Count, isel2.Count);
            Assert.AreNotEqual(isel1.Count, isel2.Count);
        }

        private IProxy Proxy_Compilation_Helper_Test(IInstantSeries str, FieldsAndPropertiesModel fom)
        {
            IProxy rts = str.NewProxy();
            rts.Target = new FieldsAndPropertiesModel();

            for (int i = 1; i < str.Rubrics.Count; i++)
            {
                var r = str.Rubrics[i].RubricInfo;
                if (r.MemberType == MemberTypes.Field)
                {
                    var fi = fom.GetType().GetField(r.Name);
                    if (fi != null)
                        rts[r.Name] = fi.GetValue(fom);
                }
                if (r.MemberType == MemberTypes.Property)
                {
                    var pi = fom.GetType().GetProperty(r.Name);
                    if (pi != null)
                        rts[r.Name] = pi.GetValue(fom);
                }
            }
            return rts;
        }
    }
}
