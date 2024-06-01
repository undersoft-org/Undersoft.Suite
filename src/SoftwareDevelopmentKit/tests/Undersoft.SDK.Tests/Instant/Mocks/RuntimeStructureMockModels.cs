namespace Undersoft.SDK.Tests.Instant
{
    using System;
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Instant.Attributes;
    using Undersoft.SDK.Rubrics.Attributes;


    public class FieldsAndPropertiesModel : Origin
    {
        public long Key = long.MaxValue;

        [InstantAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Alias { get; set; } = "ProperSize";

        [InstantAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Name { get; set; } = "SizeIsTwoTimesLonger";

        [InstantAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] ByteArray { get; set; }

        public double Testor { get; set; } = 2 * (long)int.MaxValue;

        public Guid GlobalId { get; set; } = new Guid();

        public bool Status { get; set; }
    }

    public class FieldsOnlyModel : Origin
    {

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Alias = "ProperSize";

        [InstantAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] ByteArray = new byte[10];
        public double Testor = 2 * (long)int.MaxValue;
        public Guid GlobalId = new Guid();
        public long Key = long.MaxValue;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Name = "SizeIsTwoTimesLonger";

        [DisplayRubric("AvgPrice")]
        public double Price;
        public bool Status;

    }


    public class PropertiesOnlyModel : Origin
    {

        [InstantAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Alias { get; set; } = "ProperSize";

        [InstantAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] ByteArray { get; set; }

        public double Testor { get; set; } = 2 * (long)int.MaxValue;

        public Guid GlobalId { get; set; } = new Guid();

        [KeyRubric(Order = 1)]
        [DisplayRubric("ProductName")]
        [InstantAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Name { get; set; } = "SizeIsTwoTimesLonger";

        [DisplayRubric("AvgPrice")]
        public double Price { get; set; }

        public bool Status { get; set; }

    }
}
