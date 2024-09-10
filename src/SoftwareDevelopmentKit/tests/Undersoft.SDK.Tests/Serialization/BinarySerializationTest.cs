using System.Reflection;
using System.Runtime.InteropServices;

namespace Undersoft.SDK.Tests.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocks;
using System;
using System.Collections;
using Undersoft.SDK.Extracting;
using Undersoft.SDK.Instant;
using Undersoft.SDK.Instant.Series;
using Undersoft.SDK.Serialization;
using Undersoft.SDK.Tests.Mocks.Models.Agreements;

[TestClass]
public class BinarySerializationTest
{
    internal byte[] destinationBuffer = new byte[ushort.MaxValue];
    internal byte[] sourceBuffer = new byte[ushort.MaxValue];    
    internal byte[] serializedBytesA = null;
    internal byte[] serializedBytesB = null;

    public BinarySerializationTest()
    {

    }

    [TestMethod]
    public void Serialize_Object_To_ByteArray()
    {
        Agreement seriializedAgreement = new Agreement();
        serializedBytesA = BinarySerializer.Serialize(seriializedAgreement);
        Agreement deseriializedAgreement = BinarySerializer.Deserialize<Agreement>(serializedBytesA);    
        serializedBytesB = BinarySerializer.Serialize(deseriializedAgreement);

        Assert.AreEqual(serializedBytesA.LongLength, serializedBytesB.LongLength);
    } 
}
