namespace Undersoft.SDK.Uniques
{
    using System.Collections;
    using Undersoft.SDK.Extracting;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using Hashing;
    using Undersoft.SDK;

    public unsafe static class UniqueKey32Extensions
    {
        public static Byte[] BitAggregate32to16(this Byte[] bytes)
        {
            byte[] bytes16 = new byte[2];
            fixed (byte* h16 = bytes16)
            fixed (byte* h32 = bytes)
            {
                *((ushort*)h16) = new ushort[] { *((ushort*)&h32), *((ushort*)&h32[2]) }.Aggregate(
                    (ushort)7,
                    (a, b) => (ushort)((a + b) * 7)
                );
                return bytes16;
            }
        }

        public static Byte[] BitAggregate64to16(this Byte[] bytes)
        {
            byte[] bytes16 = new byte[2];
            fixed (byte* h16 = bytes16)
            fixed (byte* h64 = bytes)
            {
                *((ushort*)h16) = new ushort[]
                {
                    *((ushort*)&h64),
                    *((ushort*)&h64[2]),
                    *((ushort*)&h64[4]),
                    *((ushort*)&h64[6])
                }.Aggregate((ushort)7, (a, b) => (ushort)((a + b) * 7));
                return bytes16;
            }
        }

        public static Int32 BitAggregate64to32(byte* bytes)
        {
            return (int)(new uint[] { *((uint*)&bytes), *((uint*)&bytes[4]) }.Aggregate(
                7U,
                (a, b) => (a + b) * 23
            ));
        }

        public static Byte[] BitAggregate64to32(this Byte[] bytes)
        {
            byte[] bytes32 = new byte[4];
            fixed (byte* h32 = bytes32)
            fixed (byte* h64 = bytes)
            {
                *((uint*)h32) = new uint[] { *((uint*)&h64), *((uint*)&h64[4]) }.Aggregate(
                    7U,
                    (a, b) => (a + b) * 23
                );
                return bytes32;
            }
        }

        public static Int32 GetHashCode(this Byte[] obj, long seed = 0)
        {
            return obj.UniqueKey32(seed);
        }

        public static Int32 GetHashCode<T>(this IEquatable<T> obj, long seed = 0)
        {
            return obj.UniqueKey32(seed);
        }

        public static Int32 GetHashCode(this IList obj, long seed = 0)
        {
            return obj.UniqueKey32(seed);
        }

        public static Int32 GetHashCode(this IntPtr ptr, int length, long seed = 0)
        {
            return ptr.UniqueKey32(length, seed);
        }

        public static Int32 GetHashCode(this IIdentifiable obj, long seed = 0)
        {
            return obj.UniqueBytes32(seed).ToInt32();
        }

        public static Int32 GetHashCode(this IUnique obj, long seed = 0)
        {
            return obj.UniqueBytes32(seed).ToInt32();
        }

        public static Int32 GetHashCode(this Object obj, long seed = 0)
        {
            return obj.UniqueKey32(seed);
        }

        public static Int32 GetHashCode(this string obj, long seed = 0)
        {
            return obj.UniqueKey32(seed);
        }

        public static Byte[] UniqueBytes32(this Byte[] bytes, long seed = 0)
        {
            return Hasher32.ComputeBytes(bytes, seed);
        }

        public static Byte[] UniqueBytes32(
            this IList obj,
            int[] sizes,
            int totalsize,
            long seed = 0
        )
        {
            byte* buffer = stackalloc byte[totalsize];
            int[] _sizes = sizes;
            int offset = 0;
            for (int i = 0; i < obj.Count; i++)
            {
                object o = obj[i];
                int s = _sizes[i];
                if (o is string)
                {
                    string str = ((string)o);
                    fixed (char* c = str)
                        Extract.CopyBlock(buffer, (byte*)c, offset, s);
                }
                else
                {
                    if (o is IUnique)
                    {
                        s = 8;
                        *((long*)(buffer + offset)) = ((IUnique)o).Id;
                    }
                    else
                    {
                        Extract.StructureToPointer(o, buffer + offset);
                    }
                }
                offset += s;
            }

            return Hasher32.ComputeBytes(buffer, offset, seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] UniqueBytes32(this IList obj, long seed = 0)
        {
            int length = 1024,
                offset = 0,
                postoffset = 0,
                count = obj.Count,
                s = 0;
            byte* buffer = stackalloc byte[length];
            bool toResize = false;

            for (int i = 0; i < count; i++)
            {
                object o = obj[i];
                var t = obj.GetType();
                if (t == typeof(string))
                {
                    string str = ((string)o);
                    s = str.Length * 2;
                    postoffset = (s + offset);

                    if (postoffset > length)
                        toResize = true;
                    else
                        fixed (char* c = str)
                            Extract.CopyBlock(buffer, (byte*)c, offset, s);
                }
                else
                {
                    if (t.IsAssignableTo(typeof(IUnique)))
                    {
                        s = 8;
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            *((long*)(buffer + offset)) = ((IUnique)o).Id;
                    }
                    else
                    {
                        if (t.IsAssignableTo(typeof(Type)))
                        {
                            o = ((Type)o).FullName;
                            s = ((Type)o).FullName.Length * 2;
                        }
                        else
                        {
                            s = o.GetSize();
                        }
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            Extract.StructureToPointer(o, buffer + offset);
                    }
                }

                if (toResize)
                {
                    i--;
                    toResize = false;
                    byte* _buffer = stackalloc byte[postoffset];
                    Extract.CopyBlock(_buffer, buffer, offset);
                    buffer = _buffer;
                    length = postoffset;
                }
                else
                    offset = postoffset;
            }

            return Hasher32.ComputeBytes(buffer, offset, seed);
        }

        public static Byte[] UniqueBytes32(this IntPtr ptr, int length, long seed = 0)
        {
            return Hasher32.ComputeBytes((byte*)ptr.ToPointer(), length, seed);
        }

        public static Byte[] UniqueBytes32(this IIdentifiable obj)
        {
            return obj.Id.GetBytes().BitAggregate64to32();
        }

        public static Byte[] UniqueBytes32(this Object obj, long seed = 0)
        {
            if (obj == null)
                return new byte[0];

            var t = obj.GetType();

            if (t.IsAssignableTo(typeof(IIdentifiable)))
                return ((IIdentifiable)obj).Id.GetBytes();
            if (t.IsValueType)
                return getValueTypeUniqueBytes32((ValueType)obj, seed);
            if (t == typeof(string))
                return (((string)obj)).UniqueBytes32(seed);
            if (t.IsAssignableTo(typeof(Type)))
                return UniqueBytes32((Type)obj, seed);
            if (t.IsAssignableTo(typeof(IList)))
            {
                if (t == typeof(Byte[]))
                    return Hasher32.ComputeBytes((Byte[])obj, seed);

                IList o = (IList)obj;
                if (o.Count == 1)
                    return UniqueBytes32(o[0], seed);

                return UniqueBytes32(o, seed);
            }
            return Hasher32.ComputeBytes(obj.GetBytes(true), seed);
        }

        public static Byte[] UniqueBytes32(this Object[] obj, long seed = 0)
        {
            if (obj.Length == 1)
                return UniqueBytes32(obj[0], seed);
            return UniqueBytes32((IList)obj, seed);
        }

        public static Byte[] UniqueBytes32(this String obj, long seed = 0)
        {
            fixed (char* c = obj)
                return Hasher32.ComputeBytes((byte*)c, obj.Length * sizeof(char), seed);
        }

        public static Byte[] UniqueBytes32(this Type obj, long seed = 0)
        {
            fixed (char* b = obj.FullName)
            {
                return Hasher32.ComputeBytes((byte*)b, obj.FullName.Length * 2, seed);
            }
        }

        public static Int32 UniqueKey32(this Byte[] obj, long seed = 0)
        {
            return Hasher32.ComputeKey(obj, (uint)seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 UniqueKey32(this IList obj, int[] sizes, int totalsize, long seed = 0)
        {
            byte* buffer = stackalloc byte[totalsize];
            int[] _sizes = sizes;
            int offset = 0;
            for (int i = 0; i < obj.Count; i++)
            {
                object o = obj[i];
                var t = obj.GetType();
                int s = _sizes[i];
                if (t == typeof(string))
                {
                    string str = ((string)o);
                    fixed (char* c = str)
                        Extract.CopyBlock(buffer, (byte*)c, offset, s);
                }
                else
                {
                    if (o is IUnique)
                    {
                        s = 8;
                        *((long*)(buffer + offset)) = ((IUnique)o).Id;
                    }
                    else
                    {
                        Extract.StructureToPointer(o, buffer + offset);
                    }
                }
                offset += s;
            }

            return (long)Hasher32.ComputeKey(buffer, offset, seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 UniqueKey32(this IList obj, long seed = 0)
        {
            int length = 256,
                offset = 0,
                postoffset = 0,
                count = obj.Count,
                s = 0;

            byte[] bytes = new byte[length];
            fixed (byte* buff = bytes)
            {
                byte* buffer = buff;
                bool toResize = false;

                for (int i = 0; i < count; i++)
                {
                    object o = obj[i];
                    var t = obj.GetType();
                    if (t == typeof(string))
                    {
                        string str = ((string)o);
                        s = str.Length * 2;
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            fixed (char* c = str)
                                Extract.CopyBlock(buffer, (byte*)c, offset, s);
                    }
                    else
                    {
                        if (t.IsAssignableTo(typeof(IUnique)))
                        {
                            s = 8;
                            postoffset = (s + offset);

                            if (postoffset > length)
                                toResize = true;
                            else
                                *((long*)(buffer + offset)) = ((IUnique)o).Id;
                        }
                        else
                        {
                            if (t.IsAssignableTo(typeof(Type)))
                            {
                                o = ((Type)o).FullName;
                                s = ((Type)o).FullName.Length * 2;
                            }
                            else
                            {
                                s = o.GetSize();
                            }

                            postoffset = (s + offset);

                            if (postoffset > length)
                                toResize = true;
                            else
                                Extract.StructureToPointer(o, buffer + offset);
                        }
                    }

                    if (toResize)
                    {
                        i--;
                        toResize = false;
                        byte[] b = new byte[postoffset];
                        fixed (byte* _buffer = b)
                        {
                            Extract.CopyBlock(_buffer, buffer, offset);
                            buffer = _buffer;
                            length = postoffset;
                        }
                    }
                    else
                        offset = postoffset;
                }

                return Hasher32.ComputeKey(buffer, offset, seed);
            }
        }

        public static Int32 UniqueKey32(this IntPtr ptr, int length, long seed = 0)
        {
            return Hasher32.ComputeKey((byte*)ptr.ToPointer(), length, seed);
        }

        public static Int32 UniqueKey32(this IIdentifiable obj)
        {
            return obj.UniqueBytes32().ToInt32();
        }

        public static Int32 UniqueKey32(this IIdentifiable obj, long seed)
        {
            return Hasher32.ComputeKey(obj.Id.GetBytes(), seed);
        }

        public static Int32 UniqueKey32(this Object obj, long seed = 0)
        {
            if (obj == null)
                return 0;

            var t = obj.GetType();

            if (t.IsAssignableTo(typeof(IIdentifiable)))
                return ((IIdentifiable)obj).UniqueBytes32().ToInt32();

            if (t.IsValueType)
                return getValueTypeUniqueKey32((ValueType)obj, seed);

            if (t == typeof(string))
                return (((string)obj)).UniqueKey32(seed);

            if (t.IsAssignableTo(typeof(Type)))
                return UniqueKey32((Type)obj, seed);

            if (t.IsAssignableTo(typeof(IList)))
            {
                if (t == typeof(Byte[]))
                    return Hasher32.ComputeKey((Byte[])obj, seed);

                IList o = (IList)obj;
                if (o.Count == 1)
                    return UniqueKey32(o[0], seed);

                return UniqueKey32(o, seed);
            }
            return Hasher32.ComputeKey(obj.GetBytes(true), seed);
        }

        public static Int32 UniqueKey32(this Object[] obj, long seed = 0)
        {
            if (obj.Length == 1)
                return UniqueKey32(obj[0], seed);
            return UniqueKey32((IList)obj, seed);
        }

        public static Int32 UniqueKey32(this string obj, long seed = 0)
        {
            fixed (char* c = obj)
                return Hasher32.ComputeKey((byte*)c, obj.Length * sizeof(char), seed);
        }

        public static Int32 UniqueKey32(this Type obj, long seed = 0)
        {
            fixed (char* b = obj.FullName)
            {
                return Hasher32.ComputeKey((byte*)b, obj.FullName.Length * sizeof(char), seed);
            }
        }

        private static Byte[] getSequentialValueTypeHashBytes32(ValueType obj, long seed = 0)
        {
            int size = obj.GetSize();
            byte[] s = new byte[size];
            fixed (byte* ps = s)
            {
                Extract.StructureToPointer(obj, ps);
                return Hasher32.ComputeBytes(ps, size, seed);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 getSequentialValueTypeUniqueKey32(ValueType obj, long seed = 0)
        {
            int size = obj.GetSize();
            byte[] s = new byte[size];
            fixed (byte* ps = s)
            {
                Extract.StructureToPointer(obj, ps);
                return Hasher32.ComputeKey(ps, size, seed);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Byte[] getValueTypeUniqueBytes32(ValueType obj, long seed = 0)
        {
            byte[] s = new byte[8];
            fixed (byte* ps = s)
            {
                Extract.StructureToPointer(obj, ps);
                return Hasher32.ComputeBytes(ps, 8, seed);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 getValueTypeUniqueKey32(ValueType obj, long seed = 0)
        {
            byte[] s = new byte[8];
            fixed (byte* ps = s)
            {
                Extract.StructureToPointer(obj, ps);
                return Hasher32.ComputeKey(ps, 8, seed);
            }
        }
    }
}
