namespace Undersoft.SDK.Extracting;

using Undersoft.SDK.Extracting.Compiler;
using System.Collections;
using System.Runtime.InteropServices;
using Uniques;

public static class ObjectExtractExtenstion
{
    public unsafe static bool CompareBlocks(
        byte* source,
        long srcOffset,
        byte* dest,
        long destOffset,
        long count
    )
    {
        long sl = count;
        long sl64 = sl / 8;
        long sl8 = sl % 8;
        long* lsrc = (long*)(source + srcOffset),
            ldst = (long*)(dest + destOffset);
        for (int i = 0; i < sl64; i++)
            if (*(&lsrc[i]) != (*(&ldst[i])))
                return false;
        byte* psrc8 = source + (sl64 * 8),
            pdst8 = dest + (sl64 * 8);
        for (int i = 0; i < sl8; i++)
            if (*(&psrc8[i]) != (*(&pdst8[i])))
                return false;
        return true;
    }

    public unsafe static Byte[] GetBytes(this IList obj, bool forKeys = false)
    {
        int length = 256,
            offset = 0,
            postoffset = 0,
            count = obj.Count,
            charsize = sizeof(char),
            s = 0;
        byte* buffer = stackalloc byte[length];
        bool toResize = false;

        for (int i = 0; i < count; i++)
        {
            object o = obj[i];
            if (o is string)
            {
                string str = ((string)o);
                s = str.Length * charsize;
                postoffset = (s + offset);

                if (postoffset > length)
                    toResize = true;
                else
                    fixed (char* c = str)
                        Extract.CopyBlock(buffer, (byte*)c, offset, s);
            }
            else
            {
                if (o is IUnique)
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
                    s = o.GetSize();
                    postoffset = (s + offset);

                    if (postoffset > length)
                        toResize = true;
                    else
                        Extract.StructureToPointer(o, new IntPtr(buffer + offset));
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

        byte[] result = new byte[offset];
        fixed (byte* result_p = result)
        {
            Extract.CopyBlock(result_p, buffer, offset);
        }
        return result;
    }

    public unsafe static int GetBytes(
        this IList objvalue,
        ref byte* buffer,
        int length,
        bool forKeys = false
    )
    {
        int offset = 0;
        int charsize = sizeof(char);

        int s;

        foreach (var o in objvalue)
        {
            s = 0;

            if (o is string)
            {
                s = ((string)o).Length * charsize;
                int fs = (s + offset);
                if (fs > length)
                {
                    byte* _buffer = stackalloc byte[fs];
                    Extract.CopyBlock(_buffer, buffer, offset);
                    buffer = _buffer;
                    length = fs;
                }

                fixed (char* c = (string)o)
                    Extract.CopyBlock(buffer, (byte*)c, offset, s);
            }
            else
            {
                if (forKeys && o is IUnique)
                {
                    s = 8;
                    *((long*)(buffer + offset)) = ((IUnique)o).Id;
                }
                else
                {
                    s = o.GetSize();
                    Extract.StructureToPointer(o, buffer + offset);
                }
            }
            offset += s;
        }

        return offset;
    }

    public unsafe static Byte[] GetBytes(this int[] objvalue)
    {
        int l = objvalue.Length * 4;
        byte[] b = new byte[l];
        fixed (byte* bp = b)
        fixed (int* lp = objvalue)
            Extract.CopyBlock(bp, (byte*)lp, l);
        return b;
    }

    public unsafe static Byte[] GetBytes(this long[] objvalue)
    {
        int l = objvalue.Length * 8;
        byte[] b = new byte[l];
        fixed (byte* bp = b)
        fixed (long* lp = objvalue)
            Extract.CopyBlock(bp, (byte*)lp, l);
        return b;
    }

    public unsafe static Byte[] GetBytes(this Object objvalue, bool forKeys = false)
    {
        Type t = objvalue.GetType();

        if (forKeys)
            if (t.IsAssignableTo(typeof(IIdentifiable)))                  
                return ((IIdentifiable)objvalue).Id.GetBytes();      
        
        if (t.IsValueType)
        {
            if (t.IsPrimitive)
                return ExtractCompiler.ValueStructureToBytes(objvalue);
            if (t == typeof(DateTime))
                return ((DateTime)objvalue).ToBinary().GetBytes();
            if (t == typeof(Enum))
                return Convert.ToInt32(objvalue).GetBytes();
            return objvalue.GetStructureBytes();
        }

        if (t == typeof(String) || t.IsAssignableTo(typeof(IFormattable)))
            return ((string)objvalue).GetBytes();

        if (t.IsLayoutSequential)
            return objvalue.GetSequentialBytes();

        if (t.IsAssignableTo(typeof(IList)))
            return ((IList)objvalue).GetBytes(forKeys);

        return objvalue.GetTrackingAddress().ToInt64().GetBytes();
    }

    public unsafe static IntPtr GetTrackingAddress(this Object objvalue)
    {
        return GCHandle.ToIntPtr(GCHandle.Alloc(objvalue, GCHandleType.Normal));
    }

    public unsafe static object GetTrackingTarget(this IntPtr ptr)
    {
        return GCHandle.FromIntPtr(ptr).Target;
    }

    public unsafe static void FreeTrackingAddress(this IntPtr ptr)
    {
        GCHandle.FromIntPtr(ptr).Free();
    }

    public unsafe static Byte[] GetBytes(this String objvalue)
    {
        int l = objvalue.Length * sizeof(char);
        byte[] b = new byte[l];
        fixed (char* c = objvalue)
        fixed (byte* pb = b)
        {
            Extract.CopyBlock(pb, (byte*)c, (uint)l);
        }
        return b;
    }

    public unsafe static Byte[] GetBytes(this uint[] objvalue)
    {
        int l = objvalue.Length * 4;
        byte[] b = new byte[l];
        fixed (byte* bp = b)
        fixed (uint* lp = objvalue)
            Extract.CopyBlock(bp, (byte*)lp, l);
        return b;
    }

    public unsafe static Byte[] GetBytes(this ulong[] objvalue)
    {
        int l = objvalue.Length * 8;
        byte[] b = new byte[l];
        fixed (byte* bp = b)
        fixed (ulong* lp = objvalue)
            Extract.CopyBlock(bp, (byte*)lp, l);
        return b;
    }

    public unsafe static byte[] GetSequentialBytes(this Object objvalue)
    {
        byte[] b = new byte[Marshal.SizeOf(objvalue)];
        fixed (byte* pb = b)
            Marshal.StructureToPtr(objvalue, new IntPtr(pb), false);
        return b;
    }

    public unsafe static int GetSize(this object structure)
    {
        return Extract.GetSize(structure);
    }

    public unsafe static int[] GetSizes(this object structure)
    {
        return Extract.GetSizes(structure);
    }

    public unsafe static byte[] GetStructureBytes(this object structure)
    {
        return Extract.GetStructureBytes(structure);
    }

    public unsafe static IntPtr GetStructureIntPtr(this object structure)
    {
        return Extract.GetStructureIntPtr(structure);
    }

    public unsafe static byte* GetStructurePointer(this object structure)
    {
        return Extract.GetStructurePointer(structure);
    }

    public unsafe static byte[] GetValueStructureBytes(this object structure)
    {
        return ExtractCompiler.ValueStructureToBytes(structure);
    }

    public unsafe static bool StructureEqual(this object structure, object other)
    {
        long asize = Extract.GetSize(structure);
        long bsize = Extract.GetSize(structure);
        if (asize < bsize)
            return false;
        byte* a = (byte*)structure.GetStructurePointer(),
            b = (byte*)other.GetStructurePointer();
        bool equal = Extract.BlockEqual(a, 0, b, 0, asize);
        Marshal.FreeHGlobal(new IntPtr(a));
        Marshal.FreeHGlobal(new IntPtr(b));
        return equal;
    }

    public unsafe static object StructureFrom(this object structure, byte* binary)
    {
        return Extract.PointerToStructure(binary, structure);
    }

    public unsafe static object StructureFrom(
        this object structure,
        byte[] binary,
        long offset = 0
    )
    {
        return Extract.BytesToStructure(binary, structure, offset);
    }

    public unsafe static object StructureFrom(this object structure, IntPtr binary)
    {
        return Extract.PointerToStructure(binary, structure);
    }

    public unsafe static void StructureTo(this object structure, byte* binary)
    {
        Extract.StructureToPointer(structure, binary);
    }

    public unsafe static void StructureTo(this object structure, IntPtr binary)
    {
        Extract.StructureToPointer(structure, binary);
    }

    public unsafe static void StructureTo(
        this object structure,
        ref byte[] binary,
        long offset = 0
    )
    {
        Extract.StructureToBytes(structure, ref binary, offset);
    }

    public unsafe static bool TryGetBytes(
        this IList objvalue,
        out byte[] bytes,
        bool forKeys = false
    )
    {
        bytes = objvalue.GetBytes(forKeys);
        if (bytes.Length > 0)
            return true;
        return false;
    }

    public unsafe static bool TryGetBytes(
        this Object objvalue,
        out Byte[] bytes,
        bool forKeys = false
    )
    {
        if ((bytes = objvalue.GetBytes(forKeys)).Length < 1)
            return false;
        return true;
    }
}
