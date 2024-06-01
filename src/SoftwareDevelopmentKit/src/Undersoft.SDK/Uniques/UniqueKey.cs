namespace Undersoft.SDK.Uniques;

using Hashing;

using System.Collections;
using Undersoft.SDK.Extracting;

public enum HashBits
{
    bit64,

    bit32
}

public interface IUniqueKey
{
    public Byte[] Bytes(Byte[] obj, long seed = 0);

    public Byte[] Bytes(IList obj, long seed = 0);

    public Byte[] Bytes(IIdentifiable obj);

    public Byte[] Bytes(object obj, long seed = 0);

    public Byte[] Bytes(string obj, long seed = 0);

    public Int64 Key(Byte[] obj, long seed = 0);

    public Int64 Key(IList obj, long seed = 0);

    public Int64 Key(IIdentifiable obj);

    public Int64 Key(IIdentifiable obj, long seed);    

    public Int64 Key(object obj, long seed = 0);

    public Int64 Key(string obj, long seed = 0);
}

public class UniqueKey32 : UniqueKey
{
    public UniqueKey32() : base(HashBits.bit32) { }

    public override Byte[] Bytes(Byte[] obj, long seed = 0)
    {
        return obj.UniqueBytes32(seed);
    }

    public override Byte[] Bytes(IList obj, long seed = 0)
    {
        return obj.UniqueBytes32(seed);
    }

    public override unsafe Byte[] Bytes(IntPtr obj, int length, long seed = 0)
    {
        return ComputeBytes((byte*)obj.ToPointer(), length, seed);
    }

    public override Byte[] Bytes(IIdentifiable obj)
    {
        return obj.Id.GetBytes();
    }

    public override Byte[] Bytes(Object obj, long seed = 0)
    {
        return obj.UniqueBytes32(seed);
    }

    public override Byte[] Bytes(string obj, long seed = 0)
    {
        return obj.UniqueBytes32(seed);
    }

    public override Byte[] Bytes(Type obj, long seed = 0)
    {
        return obj.UniqueBytes32(seed);
    }

    public override unsafe Byte[] ComputeBytes(byte* bytes, int length, long seed = 0)
    {
        return Hasher32.ComputeBytes(bytes, length, seed);
    }

    public override unsafe Byte[] ComputeBytes(byte[] bytes, long seed = 0)
    {
        return Hasher32.ComputeBytes(bytes, seed);
    }

    public override unsafe Int64 ComputeKey(byte* bytes, int length, long seed = 0)
    {
        return Hasher32.ComputeKey(bytes, length, seed);
    }

    public override unsafe Int64 ComputeKey(byte[] bytes, long seed = 0)
    {
        return Hasher32.ComputeKey(bytes, seed);
    }

    public override Int64 Key(Byte[] obj, long seed = 0)
    {
        return obj.UniqueKey32(seed);
    }

    public override Int64 Key(IList obj, long seed = 0)
    {
        return obj.UniqueKey32(seed);
    }

    public override unsafe Int64 Key(IntPtr obj, int length, long seed = 0)
    {
        return ComputeKey((byte*)obj.ToPointer(), length, seed);
    }

    public override Int64 Key(IIdentifiable obj)
    {
        return obj.Id;
    }

    public override Int64 Key(IIdentifiable obj, long seed)
    {
        return obj.Id.UniqueKey32(seed);
    }

    public override Int64 Key(Object obj, long seed = 0)
    {
        return obj.UniqueKey32(seed);
    }

    public override Int64 Key(string obj, long seed = 0)
    {
        return obj.UniqueKey32(seed);
    }

    public override Int64 Key(Type obj, long seed = 0)
    {
        return obj.UniqueKey32(seed);
    }

    protected override unsafe Byte[] Bytes(byte* obj, int length, long seed = 0)
    {
        return ComputeBytes(obj, length, seed);
    }

    protected override unsafe Int64 Key(byte* obj, int length, long seed = 0)
    {
        return ComputeKey(obj, length, seed);
    }
}

public class UniqueKey64 : UniqueKey
{
    public UniqueKey64() : base(HashBits.bit64) { }

    public override Byte[] Bytes(Byte[] obj, long seed = 0)
    {
        return obj.UniqueBytes64(seed);
    }

    public override Byte[] Bytes(IList obj, long seed = 0)
    {
        return obj.UniqueBytes64(seed);
    }

    public override unsafe Byte[] Bytes(IntPtr obj, int length, long seed = 0)
    {
        return ComputeBytes((byte*)obj.ToPointer(), length, seed);
    }

    public override Byte[] Bytes(IIdentifiable obj)
    {
        return obj.Id.GetBytes();
    }

    public override Byte[] Bytes(Object obj, long seed = 0)
    {
        return obj.UniqueBytes64(seed);
    }

    public override Byte[] Bytes(string obj, long seed = 0)
    {
        return obj.UniqueBytes64(seed);
    }

    public override Byte[] Bytes(Type obj, long seed = 0)
    {
        return obj.UniqueBytes64(seed);
    }

    public override unsafe Byte[] ComputeBytes(byte* bytes, int length, long seed = 0)
    {
        return Hasher64.ComputeBytes(bytes, length, seed);
    }

    public override Byte[] ComputeBytes(byte[] bytes, long seed = 0)
    {
        return Hasher64.ComputeBytes(bytes, seed);
    }

    public override unsafe Int64 ComputeKey(byte* bytes, int length, long seed = 0)
    {
        return (long)Hasher64.ComputeKey(bytes, length, seed);
    }

    public override Int64 ComputeKey(byte[] bytes, long seed = 0)
    {
        return (long)Hasher64.ComputeKey(bytes, seed);
    }

    public override Int64 Key(Byte[] obj, long seed = 0)
    {
        return obj.UniqueKey64(seed);
    }

    public override Int64 Key(IList obj, long seed = 0)
    {
        return obj.UniqueKey64(seed);
    }

    public override unsafe Int64 Key(IntPtr obj, int length, long seed = 0)
    {
        return ComputeKey((byte*)obj.ToPointer(), length, seed);
    }

    public override Int64 Key(IIdentifiable obj)
    {
        return obj.Id;
    }

    public override Int64 Key(IIdentifiable obj, long seed)
    {
        if (obj.TypeId != seed)
            obj.TypeId = seed;
        return obj.Id.UniqueKey64(seed);
    }

    public override Int64 Key(Object obj, long seed = 0)
    {
        return obj.UniqueKey64(seed);
    }

    public override Int64 Key(string obj, long seed = 0)
    {
        return obj.UniqueKey64(seed);
    }

    public override Int64 Key(Type obj, long seed = 0)
    {
        return obj.UniqueKey64(seed);
    }

    protected override unsafe Byte[] Bytes(byte* obj, int length, long seed = 0)
    {
        return ComputeBytes(obj, length, seed);
    }

    protected override unsafe Int64 Key(byte* obj, int length, long seed = 0)
    {
        return ComputeKey(obj, length, seed);
    }
}

public abstract class UniqueKey : IUniqueKey
{
    protected UniqueKey unique;

    public UniqueKey()
    {
        unique = Unique.Bit64;
    }

    public UniqueKey(HashBits hashBits)
    {
        if (hashBits == HashBits.bit32)
            unique = Unique.Bit32;
        else
            unique = Unique.Bit64;
    }

    public virtual Byte[] Bytes(Byte[] obj, long seed = 0)
    {
        return unique.Bytes(obj, seed);
    }

    public virtual Byte[] Bytes(IList obj, long seed = 0)
    {
        return unique.Bytes(obj, seed);
    }

    public virtual Byte[] Bytes(IntPtr obj, int length, long seed = 0)
    {
        return unique.Bytes(obj, length, seed);
    }

    public virtual Byte[] Bytes(IIdentifiable obj)
    {
        return obj.Id.GetBytes();
    }

    public virtual Byte[] Bytes(Object obj, long seed = 0)
    {
        return unique.Bytes(obj, seed);
    }

    public virtual Byte[] Bytes(string obj, long seed = 0)
    {
        return unique.Bytes(obj, seed);
    }

    public virtual Byte[] Bytes(Type obj, long seed = 0)
    {
        return unique.Bytes(obj, seed);
    }

    public virtual unsafe Byte[] ComputeBytes(byte* bytes, int length, long seed = 0)
    {
        return unique.ComputeBytes(bytes, length, seed);
    }

    public virtual unsafe Byte[] ComputeBytes(byte[] bytes, long seed = 0)
    {
        return unique.ComputeBytes(bytes, seed);
    }

    public virtual unsafe Int64 ComputeKey(byte* bytes, int length, long seed = 0)
    {
        return unique.ComputeKey(bytes, length, seed);
    }

    public virtual unsafe Int64 ComputeKey(byte[] bytes, long seed = 0)
    {
        return unique.ComputeKey(bytes, seed);
    }

    public virtual Int64 Key(Byte[] obj, long seed = 0)
    {
        return unique.Key(obj, seed);
    }

    public virtual Int64 Key(IList obj, long seed = 0)
    {
        return unique.Key(obj, seed);
    }

    public virtual Int64 Key(IntPtr obj, int length, long seed = 0)
    {
        return unique.Key(obj, length, seed);
    }

    public virtual Int64 Key(IIdentifiable obj)
    {
        return obj.Id;
    }

    public virtual Int64 Key(IIdentifiable obj, long seed)
    {
        if (obj.TypeId != seed)
            obj.TypeId = seed;
        return obj.Id.UniqueKey64(seed);
    }

    public virtual Int64 Key(Object obj, long seed = 0)
    {
        return unique.Key(obj, seed);
    }

    public virtual Int64 Key(string obj, long seed = 0)
    {
        return unique.Key(obj, seed);
    }

    public virtual Int64 Key(Type obj, long seed = 0)
    {
        return unique.Key(obj, seed);
    }

    protected virtual unsafe Byte[] Bytes(byte* obj, int length, long seed = 0)
    {
        return unique.Bytes(obj, length, seed);
    }

    protected virtual unsafe Int64 Key(byte* obj, int length, long seed = 0)
    {
        return unique.Key(obj, length, seed);
    }
}
