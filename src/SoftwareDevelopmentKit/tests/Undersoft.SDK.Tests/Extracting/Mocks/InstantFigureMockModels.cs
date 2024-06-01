using System;

namespace Undersoft.SDK.Tests.Extracting.Mocks
{
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Extracting;
    using Undersoft.SDK.Instant.Attributes;
    using Undersoft.SDK.Uniques;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct StructModel
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public char[] _alias;

        [InstantAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] ByteArray;
        public int Id;
        public long Key;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public char[] name;

        public StructModel(int id = 0)
        {
            Id = id;
            _alias = new char[10];
            name = new char[10];
            ByteArray = new byte[10];
            Key = 0;
            SerialCode = Uscn.Empty;
            Status = false;
            Time = DateTime.Now;
            GlobalId = Guid.Empty;
            Testor = 0;
        }

        public unsafe string Alias
        {
            get { return new string(_alias); }
            set
            {
                if (_alias == null)
                    _alias = new char[10];
                int al = _alias.Length;
                int l = value.Length > _alias.Length ? _alias.Length : value.Length;
                int s = sizeof(char);
                fixed (
                    char* v = value,
                        a = _alias
                )
                    Extract.Cpblk((byte*)a, (byte*)v, (uint)(l * s));
            }
        }

        public double Testor { get; set; }

        public Guid GlobalId { get; set; }

        public unsafe string Name
        {
            get { return new string(name); }
            set
            {
                if (name == null)
                    name = new char[10];
                int al = name.Length;
                int l = value.Length > name.Length ? name.Length : value.Length;
                int s = sizeof(char);
                fixed (
                    char* v = value,
                        a = name
                )
                    Extract.Cpblk((byte*)a, (byte*)v, (uint)(l * s));
            }
        }

        public Uscn SerialCode { get; set; }

        public bool Status { get; set; }

        public DateTime Time { get; set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct StructModels
    {
        [InstantAs(UnmanagedType.LPArray, SizeConst = 3)]
        public StructModel[] Structs;

        public StructModels(StructModel[] structs)
        {
            Structs = structs;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class FieldsAndPropertiesModel
    {
        public int Id { get; set; } = 404;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string Alias = "ProperSize";

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
        public string Name = "SizeIsTwoTimesLonger";

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] ByteArray;

        public double Testor { get; set; } = 2 * (long)int.MaxValue;

        public Guid GlobalId { get; set; } = new Guid();

        public bool Status { get; set; }

        public Uscn SystemKey { get; set; } = Uscn.Empty;

        public DateTime Time { get; set; } = DateTime.Now;

        public long Key = long.MaxValue;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class FieldsOnlyModel
    {
        public int Id = 404;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Alias = "ProperSize";

        [InstantAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] ByteArray = new byte[10];
        public double Testor = 2 * (long)int.MaxValue;
        public Guid GlobalId = new Guid();
        public long Key = long.MaxValue;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Name = "SizeIsTwoTimesLonger";
        public bool Status;
        public Uscn SystemKey = Uscn.Empty;
        public DateTime Time = DateTime.Now;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class PropertiesOnlyModel
    {
        public int Id { get; set; } = 404;

        [InstantAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] ByteArray { get; set; }

        public double Testor { get; set; } = 2 * (long)int.MaxValue;

        public Guid GlobalId { get; set; } = new Guid();

        [InstantAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Name { get; set; } = "SizeIsTwoTimesLonger";

        public bool Status { get; set; }

        public Uscn SystemKey { get; set; } = Uscn.Empty;

        public DateTime Time { get; set; } = DateTime.Now;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Alias = "ProperSize";

        private long Key = long.MaxValue;
    }
}
