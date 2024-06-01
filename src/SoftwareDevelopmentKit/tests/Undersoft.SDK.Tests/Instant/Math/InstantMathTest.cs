namespace Undersoft.SDK.Tests.Instant.Math;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using Undersoft.SDK.Instant;
using Undersoft.SDK.Instant.Math;
using Undersoft.SDK.Instant.Math.Set;
using Undersoft.SDK.Instant.Series;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Series;

[TestClass]
public class InstantMathTest
{
    private InstantCreator? instantCreator;
    private ProxySeriesCreator? instatnProxiesCreator;
    private InstantSeriesCreator? instatnSeriesCreator;
    private InstantMath? instantMath;
    private IInstantSeries instantValueSeries = default!;
    private IInstantSeries instantSeries = default!;
    private IInstantSeries instantProxies = default!;
    private Listing<InstantMathTestDataModel> regularList = new Listing<InstantMathTestDataModel>();

    public InstantMathTest()
    {
        createInstantProxies_Test_Helper();
        createInstantSeries_Test_Helper();
        createInstantValueSeries_Test_Helper();

        var price = nameof(InstantMathTestDataModel.NetPrice);
        var fee = nameof(InstantMathTestDataModel.SellFeeRate);

        for (int i = 0; i < 1000 * 1000; i++)
        {
            IProxy proxyRow = instantProxies.NewProxy();
            IInstant instantRow = instantSeries.NewInstant();
            IInstant instantValueRow = instantValueSeries.NewInstant();
            var regular = new InstantMathTestDataModel();
            proxyRow.Target = new InstantMathTestDataModel();

            instantRow[price] = (double)instantRow[price] + i;
            instantRow[fee] = (double)instantRow[fee] / 2;
            proxyRow[price] = instantRow[price];
            proxyRow[fee] = instantRow[fee];
            instantValueRow[price] = instantRow[price];
            instantValueRow[fee] = instantRow[fee];
            regular.NetPrice = (double)instantRow[price]; ;
            regular.SellFeeRate = (double)instantRow[fee];

            instantSeries.Add(i, instantRow);
            instantProxies.Add(i, proxyRow);
            instantValueSeries.Add(i, instantValueRow);
            regularList.Add(regular);
        }
    }

    private void createInstantProxies_Test_Helper()
    {
        instatnProxiesCreator = new ProxySeriesCreator<InstantMathTestDataModel>(false);

        instantProxies = instatnProxiesCreator.Create();
    }

    private void createInstantSeries_Test_Helper()
    {
        instatnSeriesCreator = new InstantSeriesCreator<InstantMathTestDataModel>(InstantType.Derived, false);

        instantSeries = instatnSeriesCreator.Create();
    }

    private void createInstantValueSeries_Test_Helper()
    {
        instatnSeriesCreator = new InstantSeriesCreator<InstantMathTestDataModel>(InstantType.ValueType, false);

        instantValueSeries = instatnSeriesCreator.Create();
    }

    [TestMethod]
    public void InstantMath_Generic_Member_Computation_Test_Milion_Of_Objects()
    {

        var elapsed1 = InstantMath_Generic_Member_Computation_Method(instantValueSeries);

        var elapsed2 = InstantMath_Generic_Member_Computation_Method(instantSeries);

        var elapsed3 = InstantMath_Generic_Member_Computation_Method(instantProxies);

        var watch = Stopwatch.StartNew();
        foreach (var sc in regularList.AsValues())
        {
            sc.SellNetPrice = sc.NetPrice * (sc.SellFeeRate / 100D) + sc.NetPrice;

            sc.SellGrossPrice = sc.SellNetPrice * sc.TaxRate;
        }
        watch.Stop();
        var elapsed4 = watch.Elapsed;

        Debug.WriteLine($"Instant_Math_Generic_Member_Computation_Test_Four_Milions_Of_Objects");
        Debug.WriteLine($"Instant Math on Instant Series, Value Type:{elapsed1}");
        Debug.WriteLine($"Instant Math on Instant Series, Reference Type:{elapsed2}");
        Debug.WriteLine($"Instant Math on Instant Proxies (Instant adapter for any custom data objects):{elapsed3}");
        Debug.WriteLine($"Regular Loop using standard sdk with built in math expression and operators:{elapsed4}");
    }

    private TimeSpan InstantMath_Generic_Member_Computation_Method(IInstantSeries series)
    {
        var genericInstantMath = new InstantMath<InstantMathTestDataModel>(series);

        var math0 = genericInstantMath[p => p.SellNetPrice];
        math0.Formula =
            math0[p => p.NetPrice] * (math0[p => p.SellFeeRate] / 100D) + math0[p => p.NetPrice];

        var math1 = genericInstantMath[p => p.SellGrossPrice];
        math1.Formula = math0 * math1[p => p.TaxRate];

        var watch = Stopwatch.StartNew();
        genericInstantMath.Compute();
        watch.Stop();
        return watch.Elapsed;
    }

    [TestMethod]
    public void InstantMath_Member_By_String_Computation_Test_Milion_Of_Objects()
    {

        var elapsed1 = InstantMath_Member_By_String_Computation_Method(instantValueSeries);

        var elapsed2 = InstantMath_Member_By_String_Computation_Method(instantSeries);

        var elapsed3 = InstantMath_Member_By_String_Computation_Method(instantProxies);

        var watch = Stopwatch.StartNew();
        foreach (var sc in regularList.AsValues())
        {
            sc.SellNetPrice = sc.NetPrice * (sc.SellFeeRate / 100D) + sc.NetPrice;

            sc.SellGrossPrice = sc.SellNetPrice * sc.TaxRate;
        }
        watch.Stop();
        var elapsed4 = watch.Elapsed;

        Debug.WriteLine($"Instant_Math_Member_By_String_Computation_Test_Four_Milions_Of_Objects");
        Debug.WriteLine($"Instant Math on Instant Series, Value Type:{elapsed1}");
        Debug.WriteLine($"Instant Math on Instant Series, Reference Type:{elapsed2}");
        Debug.WriteLine($"Instant Math on Instant Proxies (Instant adapter for any custom data objects):{elapsed3}");
        Debug.WriteLine($"Regular Loop using standard sdk with built in math expression and operators:{elapsed4}");
    }

    private TimeSpan InstantMath_Member_By_String_Computation_Method(IInstantSeries series)
    {
        instantMath = new InstantMath(series);

        var ms0 = instantMath["SellNetPrice"];
        ms0.Formula = ms0["NetPrice"] * (ms0["SellFeeRate"] / 100D) + ms0["NetPrice"];

        var ms1 = instantMath["SellGrossPrice"];
        ms1.Formula = ms0 * ms1["TaxRate"];

        var watch = Stopwatch.StartNew();
        instantMath.Compute();
        watch.Stop();
        return watch.Elapsed;
    }

    [TestMethod]
    public void InstantMath_Generic_Member_Parallel_Computation_In_4_Chunks_Test_Milion_Of_Objects()
    {
        var elapsed1 = InstantMath_Generic_Member_Parallel_Computation_In_4_Chunks_Method(instantValueSeries);

        var elapsed2 = InstantMath_Generic_Member_Parallel_Computation_In_4_Chunks_Method(instantSeries);

        var elapsed3 = InstantMath_Generic_Member_Parallel_Computation_In_4_Chunks_Method(instantProxies);

        var watch = Stopwatch.StartNew();
        regularList.AsParallel().ForEach((sc) =>
        {
            sc.SellNetPrice = sc.NetPrice * (sc.SellFeeRate / 100D) + sc.NetPrice;

            sc.SellGrossPrice = sc.SellNetPrice * sc.TaxRate;
        });
        watch.Stop();
        var elapsed4 = watch.Elapsed;

        Debug.WriteLine($"Instant_Math_Generic_Member_Parallel_Computation_In_4_Chunks_Test_Four_Milions_Of_Objects");
        Debug.WriteLine($"Instant Math on Instant Series, Value Type:{elapsed1}");
        Debug.WriteLine($"Instant Math on Instant Series, Reference Type:{elapsed2}");
        Debug.WriteLine($"Instant Math on Instant Proxies (Instant adapter for any custom data objects):{elapsed3}");
        Debug.WriteLine($"Prallel Loop using standard sdk with built in math expression and Linq Parallel:{elapsed4}");
    }

    private TimeSpan InstantMath_Generic_Member_Parallel_Computation_In_4_Chunks_Method(IInstantSeries series)
    {
        var genericInstantMath = new InstantMath<InstantMathTestDataModel>(series);

        var math0 = genericInstantMath[p => p.SellNetPrice];
        math0.Formula =
            math0[p => p.NetPrice] * (math0[p => p.SellFeeRate] / 100D) + math0[p => p.NetPrice];

        var math1 = genericInstantMath[p => p.SellGrossPrice];
        math1.Formula = math0 * math1[p => p.TaxRate];

        var watch = Stopwatch.StartNew();
        genericInstantMath.Compute(4);
        watch.Stop();
        return watch.Elapsed;
    }

    [TestMethod]
    public void InstantMath_Member_By_String_Computation_LogicOnStack_Test_Milion_Of_Objects()
    {
        var elapsed1 = InstantMath_Member_By_String_Computation_LogicOnStack_Method(instantValueSeries);

        var elapsed2 = InstantMath_Member_By_String_Computation_LogicOnStack_Method(instantSeries);

        var elapsed3 = InstantMath_Member_By_String_Computation_LogicOnStack_Method(instantProxies);


        var watch = Stopwatch.StartNew();
        foreach (var sc in regularList.AsValues())
        {
            sc.SellNetPrice = sc.NetPrice * (sc.SellFeeRate / 100D) + sc.NetPrice;

            sc.SellGrossPrice = sc.SellNetPrice * sc.TaxRate;
        }
        watch.Stop();
        var elapsed4 = watch.Elapsed;

        Debug.WriteLine($"Instant_Math_Member_By_String_Computation_LogicOnStack_Test_Four_Milions_Of_Objects");
        Debug.WriteLine($"Instant Math on Instant Series, Value Type:{elapsed1}");
        Debug.WriteLine($"Instant Math on Instant Series, Reference Type:{elapsed2}");
        Debug.WriteLine($"Instant Math on Instant Proxies (Instant adapter for any custom data objects):{elapsed3}");
        Debug.WriteLine($"Regular Loop using standard sdk with built in math expression and operators:{elapsed4}");
    }

    private TimeSpan InstantMath_Member_By_String_Computation_LogicOnStack_Method(IInstantSeries series)
    {
        instantMath = new InstantMath(series);

        MathSet ml = instantMath["SellNetPrice"];

        ml.Formula =
            (ml["NetPrice"] < 10 | ml["NetPrice"] > 50)
            * (ml["NetPrice"] * (ml["SellFeeRate"] / 100) + ml["NetPrice"]);

        MathSet ml2 = instantMath["SellGrossPrice"];

        ml2.Formula = ml * ml2["TaxRate"];

        var watch = Stopwatch.StartNew();
        instantMath.Compute(4);
        watch.Stop();
        return watch.Elapsed;
    }
}
