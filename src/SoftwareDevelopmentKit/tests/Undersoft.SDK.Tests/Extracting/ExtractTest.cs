using System.Reflection;
using System.Runtime.InteropServices;

namespace Undersoft.SDK.Tests.Extracting;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocks;
using System;
using Undersoft.SDK.Extracting;
using Undersoft.SDK.Instant;
using Undersoft.SDK.Instant.Series;

[TestClass]
public class ExtractTest
{
    internal byte[] destinationBuffer = new byte[ushort.MaxValue];
    internal byte[] sourceBuffer = new byte[ushort.MaxValue];
    internal IInstant? instant = null;
    internal IInstantSeries? instantSeries = null;
    internal InstantCreator? instantCreator = null;
    internal byte[]? serializedBytesA = null;
    internal byte[]? serializedBytesB = null;
    internal InstantSeriesCreator instantSeriesCreator = null;

    public ExtractTest()
    {
        Random r = new Random();
        r.NextBytes(sourceBuffer);

        instantCreator = new InstantCreator(
            InstantMocks.Instant_MemberRubric_FieldsAndPropertiesModel(),
            "Instant_MemberRubric_FieldsAndPropertiesModel_ValueType", InstantType.Reference
        );
        FieldsAndPropertiesModel fom = new FieldsAndPropertiesModel();
        instantSeriesCreator = new InstantSeriesCreator(instantCreator, "InstantSeries_Compilation_Test");
        instantSeries = instantSeriesCreator.Create();

        instant = instantSeries.NewInstant();

        foreach (var rubric in instantCreator.Rubrics.AsValues())
        {
            if (rubric.FieldId > -1)
            {
                var field = fom.GetType()
                    .GetField(
                        rubric.RubricName,
                        BindingFlags.NonPublic | BindingFlags.Instance
                    );
                if (field == null)
                    field = fom.GetType().GetField(rubric.RubricName);
                if (field == null)
                {
                    var prop = fom.GetType().GetProperty(rubric.RubricName);
                    if (prop != null)
                        instant[rubric.FieldId] = prop.GetValue(fom);
                }
                else
                    instant[rubric.FieldId] = field.GetValue(fom);
            }
        }

        for (int i = 0; i < 1000; i++)
        {
            IInstant nrcstr = instantSeries.NewInstant();
            instantSeries.Add(i, nrcstr);
        }

        serializedBytesA = new byte[instantSeries.InstantSize];
        serializedBytesB = new byte[instantSeries.InstantSize];

        instant.StructureTo(ref serializedBytesA, 0);
    }

    [TestMethod]
    public unsafe void Extract_BytesToStruct_FromType_Test()
    {
        object os = instantSeries.NewInstant();
        Extract.BytesToStructure(serializedBytesA, os, 0);
        bool equal = instant.StructureEqual(os);
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public unsafe void Extract_CopyBlock_ByteArray_UInt_Test()
    {
        Random r = new Random();
        r.NextBytes(sourceBuffer);
        destinationBuffer.Initialize();

        Extract.CopyBlock(destinationBuffer, 0, sourceBuffer, 0, sourceBuffer.Length);
        bool equal = destinationBuffer.BlockEqual(sourceBuffer);
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public unsafe void Extract_CopyBlock_ByteArray_Ulong_Test()
    {
        Random r = new Random();
        r.NextBytes(sourceBuffer);
        destinationBuffer.Initialize();

        Extract.CopyBlock(destinationBuffer, 0, sourceBuffer, 0, (ulong)sourceBuffer.Length);
        bool equal = destinationBuffer.BlockEqual(sourceBuffer);
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public unsafe void Extract_CopyBlock_Pointer_UInt_Test()
    {
        Random r = new Random();
        r.NextBytes(sourceBuffer);
        destinationBuffer.Initialize();

        fixed (
            byte* psrc = sourceBuffer,
                pdst = destinationBuffer
        )
        {
            Extract.CopyBlock(pdst, 0, psrc, 0, sourceBuffer.Length);
            bool equal = destinationBuffer.BlockEqual(sourceBuffer);
            Assert.IsTrue(equal);
        }
    }

    [TestMethod]
    public unsafe void Extract_CopyBlock_Pointer_Ulong_Test()
    {
        Random r = new Random();
        r.NextBytes(sourceBuffer);
        destinationBuffer.Initialize();

        Extract.CopyBlock(destinationBuffer, 0, sourceBuffer, 0, sourceBuffer.Length);
        bool equal = destinationBuffer.BlockEqual(sourceBuffer);
        Assert.IsTrue(equal);
    }

    [TestMethod]
    public unsafe void Extract_PointerToStructure_Type_Test()
    {
        fixed (byte* b = serializedBytesA)
        {
            object os = Extract.PointerToStructure(b, instantSeries.InstantType, 0);
            bool equal = instant.StructureEqual(os);
            Assert.IsTrue(equal);
        }
    }

    [TestMethod]
    public unsafe void Extract_PointerToStructure_Test()
    {
        fixed (byte* b = serializedBytesA)
        {
            object os = instantSeries.NewInstant();
            Extract.PointerToStructure(b, os);
            bool equal = instant.StructureEqual(os);
            Assert.IsTrue(equal);
        }
    }


    [TestMethod]
    public unsafe void Extract_StructToPointer_Test()
    {
        GCHandle gcptr = GCHandle.Alloc(serializedBytesA, GCHandleType.Pinned);
        byte* ptr = (byte*)gcptr.AddrOfPinnedObject();

        Extract.StructureToPointer(instant, ptr);

        instant["Id"] = 88888;
        instant["ServiceName"] = "Zmiany";

        Extract.StructureToPointer(instant, ptr);

        instant["Id"] = 5555555;
        instant["ServiceName"] = "Zm342";

        Extract.PointerToStructure(ptr, instant);

        Assert.AreEqual(88888, instant["Id"]);
    }
}
