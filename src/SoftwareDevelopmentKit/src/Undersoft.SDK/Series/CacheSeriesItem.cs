using Undersoft.SDK.Extracting;
using Undersoft.SDK.Logging;
using Undersoft.SDK.Uniques;
using System.Runtime.InteropServices;

namespace Undersoft.SDK.Series;

using Base;
using Invoking;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class CacheSeriesItem<V> : SeriesItemBase<V>
{
    private long _key;
    private TimeSpan duration;
    private DateTime expiration;
    private IInvoker callback;

    public void SetupExpiration(TimeSpan? lifetime, IInvoker callback = null)
    {
        duration = (lifetime != null) ? lifetime.Value : TimeSpan.FromMinutes(15);
        expiration = Log.Clock + duration;
        this.callback = callback;
    }

    private void setupExpiration()
    {
        expiration = Log.Clock + duration;
    }

    public CacheSeriesItem() : base()
    {
        SetupExpiration(TimeSpan.FromMinutes(15));
    }

    public CacheSeriesItem(ISeriesItem<V> value, TimeSpan? lifeTime = null, IInvoker deputy = null)
        : base(value)
    {
        SetupExpiration(lifeTime, deputy);
    }

    public CacheSeriesItem(object key, V value, TimeSpan? lifeTime = null, IInvoker deputy = null)
        : base(key, value)
    {
        SetupExpiration(lifeTime, deputy);
    }

    public CacheSeriesItem(long key, V value, TimeSpan? lifeTime = null, IInvoker deputy = null)
        : base(key, value)
    {
        SetupExpiration(lifeTime, deputy);
    }

    public CacheSeriesItem(V value, TimeSpan? lifeTime = null, IInvoker deputy = null) : base(value)
    {
        SetupExpiration(lifeTime, deputy);
    }

    public override long Id
    {
        get { return _key; }
        set { _key = value; }
    }

    public override V Value
    {
        get
        {
            if (Log.Clock > expiration)
            {
                Removed = true;
                if (callback != null)
                    _ = callback.InvokeAsync(value);
                return default(V);
            }
            return value;
        }
        set
        {
            setupExpiration();
            this.value = value;
        }
    }

    public override int CompareTo(ISeriesItem<V> other)
    {
        return (int)(Id - other.Id);
    }

    public override int CompareTo(object other)
    {
        return (int)(Id - other.UniqueKey64(TypeId));
    }

    public override int CompareTo(long key)
    {
        return (int)(Id - key);
    }

    public override bool Equals(object y)
    {
        return Id.Equals(y.UniqueKey64(TypeId));
    }

    public override bool Equals(long key)
    {
        return Id == key;
    }

    public override byte[] GetBytes()
    {
        return this.value.GetBytes();
    }

    public override int GetHashCode()
    {
        return (int)Id.UniqueKey32();
    }

    public unsafe override byte[] GetIdBytes()
    {
        byte[] b = new byte[8];
        fixed (byte* s = b)
            *(long*)s = _key;
        return b;
    }

    public override void Set(ISeriesItem<V> item)
    {
        Value = item.Value;
        _key = item.Id;
    }

    public override void Set(object key, V value)
    {
        this.Value = value;
        _key = key.UniqueKey64(TypeId);
    }

    public override void Set(V value)
    {
        this.Value = value;
        if (IsUnique)
            _key = ((IUnique)value).Id;
    }
}
