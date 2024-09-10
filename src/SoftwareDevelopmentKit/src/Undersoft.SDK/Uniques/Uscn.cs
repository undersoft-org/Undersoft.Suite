namespace Undersoft.SDK.Uniques
{
    using System.Collections.Specialized;
    using System.Runtime.InteropServices;
    using Undersoft.SDK;
    using Undersoft.SDK.Extracting;

    [Serializable]
    [ComVisible(true)]
    [StructLayout(LayoutKind.Sequential, Size = 32)]
    public unsafe struct Uscn
        : IFormattable,
            IComparable,
            IComparable<Uscn>,
            IEquatable<Uscn>,
            IIdentifiable,
            IUnique,
            IUniqueStructure,
            IDisposable
    {
        internal const ulong VECTOR_Y = 100000UL;
        internal const ulong VECTOR_Z = 10UL;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        private byte[] bytes;

        public string CodeNo
        {
            get => this.ToString();
            set => this.FromTetrahex(value.ToCharArray());
        }

        public long Id
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return *((long*)pbyte);
            }
            set
            {
                fixed (byte* b = sbytes)
                    *((long*)b) = value;
            }
        }

        public long TypeId
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return *((int*)(pbyte + 24));
            }
            set
            {
                fixed (byte* b = sbytes)
                    *((int*)(b + 24)) = (int)value;
            }
        }

        public long OriginId
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return *((int*)(pbyte + 28));
            }
            set
            {
                fixed (byte* b = sbytes)
                    *((int*)(b + 28)) = (int)value;
            }
        }

        public uint BlockZY
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return *((uint*)(pbyte + 8));
            }
            set
            {
                fixed (byte* b = sbytes)
                    *((uint*)(b + 8)) = value;
            }
        }

        public ulong BlockZYX
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return ((ulong)(*(uint*)(pbyte + 8)) << 16 | ((ulong)*(ushort*)(pbyte + 12)));
            }
            set
            {
                fixed (byte* b = sbytes)
                {
                    *(ushort*)(b + 12) = (ushort)value;
                    *(uint*)(b + 8) = (uint)(value >> 16);
                }
            }
        }

        public ushort BlockZ
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return *((ushort*)(pbyte + 8));
            }
            set
            {
                fixed (byte* b = sbytes)
                    *((ushort*)(b + 8)) = value;
            }
        }
        public ushort BlockY
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return *((ushort*)(pbyte + 10));
            }
            set
            {
                fixed (byte* b = sbytes)
                    *((ushort*)(b + 10)) = value;
            }
        }
        public ushort BlockX
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return *((ushort*)(pbyte + 12));
            }
            set
            {
                fixed (byte* b = sbytes)
                    *((ushort*)(b + 12)) = value;
            }
        }

        public byte Priority
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return *(pbyte + 14);
            }
            set
            {
                fixed (byte* b = sbytes)
                    *(b + 14) = value;
            }
        }

        public byte Flags
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return *(pbyte + 15);
            }
            set
            {
                fixed (byte* b = sbytes)
                    *(b + 15) = value;
            }
        }

        public long Time
        {
            get
            {
                fixed (byte* pbyte = sbytes)
                    return *((long*)(pbyte + 16));
            }
            set
            {
                fixed (byte* b = sbytes)
                    *((long*)(b + 16)) = value;
            }
        }

        public Uscn()
        {
            bytes = new byte[32];
        }

        public Uscn(long l)
        {
            bytes = new byte[32];
            fixed (byte* b = bytes)
            {
                *((long*)b) = l;
                *((long*)(b + 16)) = DateTime.Now.ToBinary();
            }
        }

        public Uscn(string s)
        {
            bytes = new byte[32];
            this.FromTetrahex(s.ToCharArray());
        }

        public Uscn(byte[] b)
        {
            bytes = new byte[32];
            if (b != null)
            {
                int l = b.Length;
                if (l > 32)
                    l = 32;
                fixed (byte* dbp = bytes)
                fixed (byte* sbp = b)
                {
                    Extract.CopyBlock(dbp, sbp, l);
                }
            }
        }

        public Uscn(long key, long seed)
        {
            bytes = new byte[32];
            fixed (byte* n = bytes)
            {
                *((long*)n) = key;
                *((long*)n + 8) = seed;
            }
        }

        public Uscn(byte[] key, long seed)
        {
            bytes = new byte[32];
            fixed (byte* n = bytes)
            {
                fixed (byte* s = key)
                    *((long*)n) = *((long*)s);
                *((long*)(n + 8)) = seed;
            }
        }

        public Uscn(object key, long seed)
        {
            bytes = new byte[32];
            byte[] shah = key.UniqueBytes64();
            fixed (byte* n = bytes)
            {
                fixed (byte* s = shah)
                    *((long*)n) = *((long*)s);
                *((long*)(n + 8)) = seed;
            }
        }

        public Uscn(long key, short z, short y, short x, short flags, long time)
        {
            bytes = new byte[32];
            fixed (byte* n = bytes)
            {
                *((long*)n) = key;
                *((short*)&n[8]) = z;
                *((short*)&n[10]) = y;
                *((short*)&n[12]) = x;
                *((short*)&n[14]) = flags;
                *((long*)&n[16]) = time;
            }
        }

        public Uscn(byte[] key, short z, short y, short x, short flags, long time)
        {
            bytes = new byte[32];
            fixed (byte* n = bytes)
            {
                fixed (byte* s = key)
                    *((long*)n) = *((long*)s);
                *((short*)(n + 8)) = z;
                *((short*)(n + 10)) = y;
                *((short*)(n + 12)) = x;
                *((short*)(n + 14)) = flags;
                *((long*)(n + 16)) = time;
            }
        }

        public Uscn(object key, short z, short y, short x, BitVector32 flags, DateTime time)
        {
            bytes = new byte[32];
            byte[] shah = key.UniqueBytes64();
            fixed (byte* n = bytes)
            {
                fixed (byte* s = shah)
                    *((ulong*)n) = *((ulong*)s);
                *((short*)(n + 8)) = z;
                *((short*)(n + 10)) = y;
                *((short*)(n + 12)) = x;
                *((short*)(n + 14)) = *((short*)&flags);
                *((long*)(n + 16)) = time.ToBinary();
            }
        }

        public Uscn(object key)
        {
            bytes = new byte[32];
            fixed (byte* n = bytes)
            {
                *((long*)n) = key.UniqueKey64();
            }
        }

        public Uscn(Uscn item)
        {
            bytes = new byte[32];
            fixed (byte* pbyte = bytes)
            fixed (byte* pbytes = item.GetBytes())
                Extract.CopyBlock(pbyte, pbytes, 32);
        }

        public byte[] this[int offset]
        {
            get
            {
                if (offset != 0)
                {
                    int l = 32 - offset;
                    byte[] r = new byte[l];
                    fixed (byte* pbyte = sbytes)
                    fixed (byte* rbyte = r)
                    {
                        Extract.CopyBlock(rbyte, pbyte + offset, l);
                    }

                    return r;
                }

                return null;
            }
            set
            {
                int l = value.Length;
                if (offset > 0 && l < 32)
                {
                    int count = 32 - offset;
                    if (l < count)
                        count = l;
                    fixed (byte* pbyte = sbytes)
                    fixed (byte* rbyte = value)
                    {
                        Extract.CopyBlock(pbyte, rbyte, offset, count);
                    }
                }
                else
                {
                    fixed (byte* pbyte = sbytes)
                    fixed (byte* rbyte = value)
                    {
                        Extract.CopyBlock(pbyte, rbyte, 32);
                    }
                }
            }
        }
        public byte[] this[int offset, int length]
        {
            get
            {
                if (offset < 32)
                {
                    if ((32 - offset) > length)
                        length = 32 - offset;

                    byte[] r = new byte[length];
                    fixed (byte* pbyte = sbytes)
                    fixed (byte* rbyte = r)
                    {
                        Extract.CopyBlock(rbyte, pbyte + offset, length);
                    }
                    return r;
                }
                return null;
            }
            set
            {
                if (offset < 32)
                {
                    if ((32 - offset) > length)
                        length = 32 - offset;
                    if (value.Length < length)
                        length = value.Length;

                    fixed (byte* rbyte = value)
                    fixed (byte* pbyte = sbytes)
                    {
                        Extract.CopyBlock(pbyte, rbyte, offset, length);
                    }
                }
            }
        }

        private byte[] sbytes
        {
            get => bytes ??= new byte[32];
        }

        public void SetBytes(byte[] value, int offset)
        {
            this[offset] = value;
        }

        public byte[] GetBytes(int offset, int length)
        {
            return this[offset, length];
        }

        public byte[] GetBytes()
        {
            byte[] r = new byte[32];
            fixed (byte* rbyte = r)
            fixed (byte* pbyte = sbytes)
            {
                Extract.CopyBlock(rbyte, pbyte, 32);
            }
            return r;
        }

        public byte[] GetIdBytes()
        {
            byte[] kbytes = new byte[8];
            fixed (byte* b = sbytes)
            fixed (byte* k = kbytes)
                *((ulong*)k) = *((ulong*)b);
            return kbytes;
        }

        public long SetId(long value)
        {
            return Id = value;
        }

        public void SetCodeNo(string value)
        {
            this.FromTetrahex(value.ToCharArray());
        }

        public long GetId()
        {
            return Id;
        }

        public long SetTypeId(long seed)
        {
            return TypeId = seed;
        }

        public long SetOriginId(long key)
        {
            return OriginId = key;
        }

        public long GetTypeId()
        {
            return TypeId;
        }

        public ulong GetBlockId()
        {
            return GetBlockId(VECTOR_Z, VECTOR_Y);
        }

        public ulong SetBlockId(ulong index)
        {
            return SetBlockId(VECTOR_Z, VECTOR_Y, index);
        }

        public ulong GetBlockId(ulong vectorZ, ulong vectorY)
        {
            return (BlockZ * vectorZ * vectorY) + (BlockY * vectorY) + BlockX;
        }

        public ulong SetBlockId(ulong vectorZ, ulong vectorY, ulong value)
        {
            if (value > 0)
            {
                ulong vectorYZ = (vectorY * vectorZ);
                ulong blockZ = (ushort)Math.Ceiling(value / (double)vectorYZ);
                ulong blockYZsub = value - (blockZ * vectorYZ);
                ulong blockY = (ushort)Math.Ceiling(blockYZsub / (double)vectorY);
                ulong blockX = value % vectorY;

                ulong zyx = (blockZ << 32) | (blockY << 16) | (blockX);
                BlockZYX = zyx;
                return zyx;
            }
            return 0;
        }

        public ushort GetFlags()
        {
            fixed (byte* pbyte = sbytes)
                return *((ushort*)(pbyte + 15));
        }

        public BitVector32 GetFlagsBits()
        {
            fixed (byte* pbyte = sbytes)
                return new BitVector32(*((pbyte + 15)));
        }

        public void SetFlagBits(BitVector32 bits)
        {
            fixed (byte* pbyte = sbytes)
                *((pbyte + 15)) = *((byte*)&bits);
        }

        public void SetFlagBit(ushort flagNumber)
        {
            fixed (byte* pbyte = sbytes)
            {
                *((pbyte + 15)) = (byte)(*((pbyte + 15)) | (1 << flagNumber));
            }
        }

        public void ClearFlagBit(ushort flagNumber)
        {
            fixed (byte* pbyte = sbytes)
            {
                *((pbyte + 15)) = (byte)(*((pbyte + 15)) & ~(1 << flagNumber));
            }
        }

        public bool GetFlagBit(ushort flagNumber)
        {
            fixed (byte* pbyte = sbytes)
            {
                int value = ((*((pbyte + 15)) >> flagNumber) & 1);
                return (value > 0) ? true : false;
            }
        }

        public bool GetFlag(DataFlags flag)
        {
            return GetFlagBit((ushort)flag);
        }

        public void SetFlag(DataFlags flag, bool isOn)
        {
            if (isOn)
                SetFlagBit((ushort)flag);
            else
                ClearFlagBit((ushort)flag);
        }

        public void SetFlag(bool flag, ushort flagNumber)
        {
            if (flag)
                SetFlagBit(flagNumber);
            else
                ClearFlagBit(flagNumber);
        }

        public byte GetPriority()
        {
            fixed (byte* pbyte = sbytes)
            {
                return *(pbyte + 15);
            }
        }

        public byte SetPriority(byte priority)
        {
            fixed (byte* pbyte = sbytes)
            {
                return *(pbyte + 15) = priority;
            }
        }

        public byte ComparePriority(byte priority)
        {
            fixed (byte* pbyte = sbytes)
            {
                return (byte)(*(pbyte + 15) - priority);
            }
        }

        public long GetDateLong()
        {
            fixed (byte* pbyte = bytes)
                return *((long*)(pbyte + 16));
        }

        public DateTime GetDateTime()
        {
            fixed (byte* pbyte = sbytes)
                return DateTime.FromBinary(*((long*)(pbyte + 16)));
        }

        public void SetDateLong(long item)
        {
            fixed (byte* pbyte = sbytes)
                *((long*)(pbyte + 16)) = item;
        }

        public Guid GetGuid()
        {
            if (IsEmpty)
                return Guid.Empty;
            byte[] dst = new byte[16];
            fixed (byte* d = dst)
            fixed (byte* b = sbytes)
            {
                *(ulong*)d = *(ulong*)b;
                *(ulong*)(d + 8) = *(ulong*)(b + 24);
            }

            return new Guid(dst);
        }

        public void SetGuid(Guid guid)
        {
            byte[] src = guid.ToByteArray();
            fixed (byte* s = src)
            fixed (byte* b = sbytes)
            {
                *(ulong*)b = *(ulong*)s;
                *(ulong*)(b + 24) = *(ulong*)(s + 8);
            }
        }

        public Guid GUID
        {
            get => GetGuid();
            set => SetGuid(value);
        }

        public bool IsNotEmpty
        {
            get { return (Id != 0); }
        }

        public bool IsEmpty
        {
            get { return (Id == 0); }
        }

        public override int GetHashCode()
        {
            fixed (byte* pbyte = &this[0, 8].BitAggregate64to32()[0])
                return *((int*)pbyte);
        }

        public int CompareTo(object value)
        {
            if (value == null)
                return 1;
            if (!(value is Uscn))
                throw new Exception();

            return (int)(Id - ((Uscn)value).Id);
        }

        public int CompareTo(Uscn g)
        {
            return (int)(Id - g.Id);
        }

        public int CompareTo(IUnique g)
        {
            return (int)(Id - g.Id);
        }

        public int CompareTo(IIdentifiable g)
        {
            return (int)(Id - g.Id);
        }

        public bool Equals(long g)
        {
            return (Id == g);
        }

        public override bool Equals(object value)
        {
            var type = value.GetType();
            if (value == null)
                return false;
            if ((type == typeof(string)))
                return new Uscn(value.ToString()).Id == Id;
            if (type.IsAssignableTo(typeof(ulong)))
                return (Id == (long)value);
            if (type.IsAssignableTo(typeof(long)))
                return ((long)Id == (long)value);
            return ReferenceEquals(Id, value);
        }

        public bool Equals(IUnique g)
        {
            return (Id == g.Id);
        }

        public bool Equals(IIdentifiable g)
        {
            return (Id == g.Id);
        }

        public override String ToString()
        {
            return new string(this.ToTetrahex());
        }

        public String ToString(String format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return new string(this.ToTetrahex());
        }

        public static bool operator ==(Uscn a, Uscn b)
        {
            return (a.Id == b.Id);
        }

        public static bool operator !=(Uscn a, Uscn b)
        {
            return (a.Id != b.Id);
        }

        public static explicit operator Uscn(String s)
        {
            return new Uscn(s);
        }

        public static implicit operator String(Uscn s)
        {
            return s.ToString();
        }

        public static explicit operator Uscn(byte[] l)
        {
            return new Uscn(l);
        }

        public static implicit operator byte[](Uscn s)
        {
            return s.GetBytes();
        }

        public static Uscn Empty => new Uscn() { bytes = new byte[32] };

        public static Uscn New => new Uscn(Unique.NewId) { Time = DateTime.Now.ToBinary() };

        public Uscn Auto()
        {
            Id = (long)Unique.NewId;
            Time = DateTime.Now.ToBinary();
            return this;
        }

        public char[] ToTetrahex()
        {
            char[] pchchar = new char[32];
            ulong pchblock;
            int pchlength = 32;
            byte pchbyte;
            int idx = 0;

            for (int j = 0; j < 4; j++)
            {
                fixed (byte* pbyte = sbytes)
                {
                    pchblock = *((ulong*)(pbyte + (j * 6)));
                }
                pchblock = pchblock & 0x0000ffffffffffffL;
                for (int i = 0; i < 8; i++)
                {
                    pchbyte = (byte)(pchblock & 0x3fL);
                    pchchar[idx] = (pchbyte).ToTetrahexChar();
                    idx++;
                    pchblock = pchblock >> 6;
                    if (pchbyte != 0x00)
                        pchlength = idx;
                }
            }

            char[] pchchartrim = new char[pchlength];
            Array.Copy(pchchar, 0, pchchartrim, 0, pchlength);

            return pchchartrim;
        }

        public void FromTetrahex(char[] pchchar)
        {
            int pchlength = pchchar.Length;
            int idx = 0;
            byte pchbyte;
            ulong pchblock = 0;
            int blocklength = 8;
            uint pchblock_int;
            ushort pchblock_short;

            for (int j = 0; j < 4; j++)
            {
                pchblock = 0x00L;
                blocklength = Math.Min(8, Math.Max(0, pchlength - 8 * j));
                idx = Math.Min(pchlength, 8 * (j + 1)) - 1;

                for (int i = 0; i < blocklength; i++)
                {
                    pchbyte = (pchchar[idx]).ToTetrahexByte();
                    pchblock = pchblock << 6;
                    pchblock = pchblock | (pchbyte & 0x3fUL);
                    idx--;
                }
                fixed (byte* pbyte = sbytes)
                {
                    if (j == 3)
                    {
                        pchblock_short = (ushort)(pchblock & 0x00ffffUL);
                        pchblock_int = (uint)(pchblock >> 16);
                        *((ulong*)&pbyte[18]) = pchblock_short;
                        *((ulong*)&pbyte[20]) = pchblock_int;
                        break;
                    }
                    *((ulong*)&pbyte[j * 6]) = pchblock;
                }
            }
        }

        public bool Equals(Uscn g)
        {
            if (g.IsEmpty)
                return false;
            fixed (byte* spbyte = sbytes)
            fixed (byte* pbyte = g.GetBytes())
            {
                return
               *((ulong*)&spbyte[0]) == *((ulong*)&pbyte[0]) &&
               *((ulong*)&spbyte[8]) == *((ulong*)&pbyte[8]) &&
               *((ulong*)&spbyte[16]) == *((ulong*)&pbyte[16]) &&
               *((ulong*)&spbyte[24]) == *((ulong*)&pbyte[24]);
            }
        }

        public bool Equals(BitVector32 other)
        {
            return GetFlagsBits().Equals(other);
        }

        public bool Equals(DateTime other)
        {
            return other.ToBinary() == Time;
        }

        public bool Equals(IUniqueStructure other)
        {
            return base.Equals(new Uscn(other.GetBytes()));
        }

        public void Dispose()
        {
            bytes = null;
        }
    }
}
