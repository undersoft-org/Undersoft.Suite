namespace Undersoft.SDK.Tests.Instant
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Reflection;
    using Undersoft.SDK.Instant;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Proxies;

    [TestClass]
    public class InstantSeriesTest
    {
        private IInstant instant;
        private IInstantSeries instantSeries;
        private InstantSeriesCreator instantSeriesCreator;
        private InstantCreator instantCreator;

        [TestMethod]
        public void InstantSeries_Compile_Test()
        {
            var Instant2 = new ProxyCreator<FieldsAndPropertiesModel>();

            FieldsAndPropertiesModel fom = new FieldsAndPropertiesModel();

            var IInstant2 = Proxy_Compilation_Helper_Test(Instant2, fom);

            var IProxy = new FieldsAndPropertiesModel().ToProxy();

            var InstantSeries2 = new ProxySeriesCreator<FieldsAndPropertiesModel>();

            var rttab = InstantSeries2.Create();

            for (int i = 0; i < 10000; i++)
            {
                rttab.Add((long)int.MaxValue + i, rttab.NewInstant());
            }

            for (int i = 9999; i > -1; i--)
            {
                rttab[i] = rttab.Get(i + (long)int.MaxValue);
            }
        }

        [TestMethod]
        public void InstantSeries_MutatorAndAccessorById_Test()
        {
            instantCreator = new InstantCreator(typeof(FieldsAndPropertiesModel));
            FieldsAndPropertiesModel fom = new FieldsAndPropertiesModel();
            instant = Instant_Compilation_Helper_Test(instantCreator, fom);

            instantSeriesCreator = new InstantSeriesCreator(
                instantCreator,
                "InstantSequence_Compilation_Test"
            );

            instantSeries = instantSeriesCreator.Create();

            instantSeries.Add(instantSeries.NewInstant());
            instantSeries[0, 4] = instant[4];

            Assert.AreEqual(instant[4], instantSeries[0, 4]);
        }

        [TestMethod]
        public void InstantSeries_MutatorAndAccessorByName_Test()
        {
            instantCreator = new InstantCreator(typeof(FieldsAndPropertiesModel));
            FieldsAndPropertiesModel fom = new FieldsAndPropertiesModel();
            instant = Instant_Compilation_Helper_Test(instantCreator, fom);

            instantSeriesCreator = new InstantSeriesCreator(
                instantCreator,
                "InstantSequence_Compilation_Test"
            );

            instantSeries = instantSeriesCreator.Create();

            instantSeries.Add(instantSeries.NewInstant());
            instantSeries[0, nameof(fom.Name)] = instant[nameof(fom.Name)];

            Assert.AreEqual(instant[nameof(fom.Name)], instantSeries[0, nameof(fom.Name)]);
        }

        [TestMethod]
        public void InstantSeries_NewItem_Test()
        {
            instantCreator = new InstantCreator(typeof(FieldsAndPropertiesModel));
            FieldsAndPropertiesModel fom = new FieldsAndPropertiesModel();
            instant = Instant_Compilation_Helper_Test(instantCreator, fom);

            instantSeriesCreator = new InstantSeriesCreator(
                instantCreator,
                "InstantSequence_Compilation_Test"
            );

            instantSeries = instantSeriesCreator.Create();

            IInstant rcst = instantSeries.NewInstant();

            Assert.IsNotNull(rcst);
        }

        [TestMethod]
        public void InstantSeries_SetRubrics_Test()
        {
            instantCreator = new InstantCreator(typeof(FieldsAndPropertiesModel));
            FieldsAndPropertiesModel fom = new FieldsAndPropertiesModel();
            instant = Instant_Compilation_Helper_Test(instantCreator, fom);

            instantSeriesCreator = new InstantSeriesCreator(
                instantCreator,
                "InstantSequence_Compilation_Test"
            );

            var rttab = instantSeriesCreator.Create();
            Assert.IsTrue(rttab.Rubrics.All(
                r =>
                    r.RubricId == instantSeriesCreator.Rubrics[r.RubricId].RubricId
                    && r.RubricName == instantSeriesCreator.Rubrics[r.RubricId].RubricName
                    && r.RubricType == instantSeriesCreator.Rubrics[r.RubricId].RubricType
            ));
        }

        private IInstant Instant_Compilation_Helper_Test(
            InstantCreator Instant,
            FieldsAndPropertiesModel fom
        )
        {
            IInstant rts = Instant.Create();

            for (int i = 1; i < Instant.Rubrics.Count; i++)
            {
                var r = Instant.Rubrics[i].RubricInfo;
                if (r.MemberType == MemberTypes.Field)
                {
                    var fi = fom.GetType().GetField(((FieldInfo)r).Name);
                    if (fi != null)
                        rts[r.Name] = fi.GetValue(fom);
                }
                if (r.MemberType == MemberTypes.Property)
                {
                    var pi = fom.GetType().GetProperty(((PropertyInfo)r).Name);
                    if (pi != null)
                        rts[r.Name] = pi.GetValue(fom);
                }
            }
            return rts;
        }

        private IInstant Proxy_Compilation_Helper_Test(
            ProxyCreator Instant2,
            FieldsAndPropertiesModel fom
        )
        {
            List<IProxy> list = new List<IProxy>();
            for (int y = 0; y < 300000; y++)
            {
                var rts0 = Instant2.Create();

                for (int i = 1; i < Instant2.Rubrics.Count; i++)
                {
                    var r = Instant2.Rubrics[i].RubricInfo;
                    if (r.MemberType == MemberTypes.Field)
                    {
                        var fi = fom.GetType().GetField(((FieldInfo)r).Name);
                        if (fi != null)
                            rts0[r.Name] = fi.GetValue(fom);
                    }
                    if (r.MemberType == MemberTypes.Property)
                    {
                        var pi = fom.GetType().GetProperty(((PropertyInfo)r).Name);
                        if (pi != null)
                            rts0[r.Name] = pi.GetValue(fom);
                    }
                }

                list.Add(rts0);
            }
            return list[0];
        }
    }
}
