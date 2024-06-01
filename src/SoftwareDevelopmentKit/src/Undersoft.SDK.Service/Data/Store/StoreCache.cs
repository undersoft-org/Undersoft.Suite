using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Data.Cache;
using Undersoft.SDK.Service.Data.Mapper;
using Undersoft.SDK.Uniques;
using System;

namespace Undersoft.SDK.Service.Data.Store;

using Invoking;
using Undersoft.SDK;

public class StoreCache<TStore> : DataCache, IStoreCache<TStore>
{
    public StoreCache(IDataCache cache)
    {
        if (base.cache == null || this.cache == null)
        {
            Mapper = DataMapper.GetMapper();
            base.cache = cache;
            int seed = typeof(TStore).UniqueKey32();
            TypeId = seed;
            if (!base.Catalog.TryGet(seed, out IIdentifiable deck))
            {
                deck = new TypedRegistry<IIdentifiable>();
                base.Catalog.Add(seed, deck);
            }
            this.cache = (ITypedSeries<IIdentifiable>)deck;
        }
    }

    public StoreCache(TimeSpan? lifeTime = null, Invoker callback = null, int capacity = 257) : base(
        lifeTime,
        callback,
        capacity)
    {
        if (cache == null)
        {
            int seed = typeof(TStore).UniqueKey32();
            TypeId = seed;
            if (!base.Catalog.TryGet(seed, out IIdentifiable deck))
            {
                deck = new TypedRegistry<IIdentifiable>();
                base.Catalog.Add(seed, deck);
            }
            cache = (ITypedSeries<IIdentifiable>)deck;
        }
    }

    protected override ITypedSeries<IIdentifiable> cache { get; set; }

    public override ITypedSeries<IIdentifiable> Catalog => cache;

    public IDataMapper Mapper { get; set; }
}
