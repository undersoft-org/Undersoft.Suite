using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Undersoft.SDK.Series;
using System.Threading.Tasks;
using Undersoft.SDK.Uniques;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Data.Entity
{
    public class EntityCache<TStore, TEntity> : StoreCache<TStore>, IEntityCache<TStore, TEntity>
        where TEntity : IIdentifiable
    {
        public EntityCache(IStoreCache<TStore> datacache) : base()
        {
            if (base.cache == null || cache == null)
            {
                Mapper = datacache.Mapper;
                base.cache = datacache;
                int seed = typeof(TEntity).GetDataTypeId();
                if (!base.Catalog.TryGet(seed, out IIdentifiable deck))
                {
                    deck = new TypedRegistry<IIdentifiable>();
                    base.Catalog.Add(seed, deck);
                }
                cache = (ITypedSeries<IIdentifiable>)deck;

                TypeId = seed;
                base.TypeId = typeof(TStore).GetDataTypeId();
            }
        }

        protected override ITypedSeries<IIdentifiable> cache { get; set; }

        public ITypedSeries<IIdentifiable> CacheSet()
        {
            return CacheSet<TEntity>();
        }

        public TEntity Lookup(object keys)
        {
            return Lookup<TEntity>(keys);
        }

        public ISeries<IIdentifiable> Lookup(Tuple<string, object> valueNamePair)
        {
            return Lookup<TEntity>(
                (m) => (ISeries<IIdentifiable>)m.Get(valueNamePair.Item2, valueNamePair.Item1.UniqueKey32())
            );
        }

        public ISeries<IIdentifiable> Lookup(Func<ITypedSeries<IIdentifiable>, ISeries<IIdentifiable>> selector)
        {
            return Lookup<TEntity>(selector);
        }

        public TEntity Lookup(TEntity item)
        {
            return Lookup<TEntity>(item);
        }

        public TEntity[] Lookup(object[] key, params Tuple<string, object>[] valueNamePairs)
        {
            return Lookup<TEntity>(key, valueNamePairs);
        }

        public TEntity[] Lookup(
            Func<ISeries<IIdentifiable>, IIdentifiable> key,
            params Func<ITypedSeries<IIdentifiable>, ISeries<IIdentifiable>>[] selectors
        )
        {
            return Lookup<TEntity>(key, selectors);
        }

        public ISeries<IIdentifiable> Lookup(object key, string propertyNames)
        {
            return Lookup<TEntity>(key, propertyNames);
        }

        public TEntity Lookup(TEntity item, params string[] propertyNames)
        {
            return Lookup<TEntity>(item, propertyNames);
        }

        public IEnumerable<TEntity> Memorize(IEnumerable<TEntity> items)
        {
            return Memorize<TEntity>(items);
        }

        public TEntity Memorize(TEntity item)
        {
            return Memorize<TEntity>(item);
        }

        public TEntity Memorize(TEntity item, params string[] names)
        {
            return Memorize<TEntity>(item, names);
        }

        public async Task<TEntity> MemorizeAsync(TEntity item)
        {
            return await MemorizeAsync<TEntity>(item);
        }

        public async Task<TEntity> MemorizeAsync(TEntity item, params string[] names)
        {
            return await MemorizeAsync<TEntity>(item, names);
        }

        public override ITypedSeries<IIdentifiable> Catalog => cache;

        public override long TypeId { get; set; }
    }
}
