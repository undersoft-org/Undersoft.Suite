using Microsoft.OData.Client;
using System.Collections;
using System.Linq.Expressions;
using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Remote.Repository;

namespace Undersoft.SDK.Service.Data.Remote
{
    public interface IRemoteSet<TStore, TEntity> : IRemoteSet<TEntity> { }

    public interface IRemoteSet<TEntity>
        : ICollection<TEntity>,
            IEnumerable<TEntity>,
            IEnumerable,
            IList<TEntity>
    {
        DataServiceContext Context { get; }

        void LoadAsync<TOrigin>(
            TOrigin origin,
            Func<TOrigin, Expression<Func<TEntity, bool>>> predicate
        );

        void Load<TOrigin>(
            TOrigin origin,
            Func<TOrigin, Expression<Func<TEntity, bool>>> predicate
        );

        IQueryable<TEntity> Load(Expression<Func<TEntity, bool>> predicate);

        void LoadAsync(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Load(Func<IQueryable<TEntity>, IQueryable<TEntity>> query);

        void LoadAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query);

        bool TryGet(object key, out TEntity value);

        bool ContainsKey(object key);

        event EventHandler<LoadCompletedEventArgs> LoadCompleted;

        void LoadAsync(int offset = 0, int limit = 0);

        void Load(int offset = 0, int limit = 0);

        Task SaveAsync();

        void Save();
    }

    public class RemoteSet<TStore, TEntity> : RemoteSet<TEntity>, IRemoteSet<TStore, TEntity>
        where TEntity : class, IOrigin, IInnerProxy
    {
        private IRemoteRepository<TEntity> _repository;

        public RemoteSet(IRemoteRepository<TStore, TEntity> repository) : base(repository.Query)
        {
            _repository = repository;
        }
    }

    public class RemoteSet<TEntity>
        : DataServiceCollection<TEntity>,
            IRemoteSet<TEntity>,
            IFindable<TEntity> where TEntity : class, IOrigin
    {
        protected DataServiceContext context;
        protected DataServiceQuery<TEntity> _query;
        protected ISeries<TEntity> _listing = new Listing<TEntity>();
        protected object origin;
        protected string name;

        public DataServiceContext Context => context;

        public ISeries<TEntity> GetListing() => _listing;

        object IFindable.this[object key]
        {
            get
            {
                return (IFindable<TEntity>)this[key];
            }
            set
            {
                var entity = this[key];
                if (entity != null)
                    entity.PatchFrom(value);
            }
        }

        public TEntity this[object key]
        {
            get
            {
                if (!_listing.TryGet(key, out TEntity entity))
                {
                    Load(_query.Where(e => e.Id == (long)key));
                    if (!_listing.TryGet(key, out entity))
                    {
                        _listing.Put(this);
                        entity = _listing[key];
                    }
                }
                return entity;
            }
            set
            {
                var entity = this[key];
                if (entity != null)
                    entity.PatchFrom(value);
            }
        }

        public TEntity this[object[] keys]
        {
            get
            {
                if (keys.Length < 2)
                    return this[keys[0]];

                if (!_listing.TryGet(keys, out TEntity entity))
                {
                    Load(_query.Where(e => keys.Contains(e.Id)));
                    if (!_listing.TryGet(keys, out entity))
                    {
                        _listing.Put(this);
                        entity = _listing[keys];
                    }
                }
                return entity;
            }
            set
            {
                var entity = this[keys];
                if (entity != null)
                    entity.PatchFrom(value);
            }
        }

        public IQueryable<TEntity> this[Func<IQueryable<TEntity>, IQueryable<TEntity>> query]
        {
            get
            {
                Load(query(_query));
                return query(this.AsQueryable());
            }
            set
            {
                Load(query(value));
            }
        }

        public RemoteSet() : base() { }

        public RemoteSet(DataServiceQuery<TEntity> query) : base(query.Context)
        {
            _query = query;
            context = query.Context;
        }

        public RemoteSet(DataServiceContext context, IQueryable<TEntity> query) : base(context)
        {
            _query = (DataServiceQuery<TEntity>)query;
            this.context = context;
        }

        public RemoteSet(DataServiceQuerySingle<TEntity> item) : base(item)
        {
            context = item.Context;
            _query = context.CreateQuery<TEntity>(typeof(TEntity).Name);
        }

        public RemoteSet(IEnumerable<TEntity> items) : base(items) { }

        public RemoteSet(TrackingMode trackingMode, DataServiceQuerySingle<TEntity> item)
            : base(trackingMode, item)
        {
            context = item.Context;
            _query = context.CreateQuery<TEntity>(typeof(TEntity).Name);
        }

        public RemoteSet(IEnumerable<TEntity> items, TrackingMode trackingMode)
            : base(items, trackingMode) { }

        public RemoteSet(DataServiceContext context) : base(context)
        {
            this.context = context;
            _query = context.CreateQuery<TEntity>(typeof(TEntity).Name);
        }

        public RemoteSet(
            DataServiceContext context,
            string entitySetName,
            Func<EntityChangedParams, bool> entityChangedCallback,
            Func<EntityCollectionChangedParams, bool> collectionChangedCallback
        ) : base(context, entitySetName, entityChangedCallback, collectionChangedCallback)
        {
            this.context = context;
            _query = context.CreateQuery<TEntity>(entitySetName);
        }

        public RemoteSet(
            IEnumerable<TEntity> items,
            TrackingMode trackingMode,
            string entitySetName,
            Func<EntityChangedParams, bool> entityChangedCallback,
            Func<EntityCollectionChangedParams, bool> collectionChangedCallback
        )
            : base(
                items,
                trackingMode,
                entitySetName,
                entityChangedCallback,
                collectionChangedCallback
            )
        { }

        public RemoteSet(
            DataServiceContext context,
            IEnumerable<TEntity> items,
            TrackingMode trackingMode,
            string entitySetName,
            Func<EntityChangedParams, bool> entityChangedCallback,
            Func<EntityCollectionChangedParams, bool> collectionChangedCallback
        )
            : base(
                context,
                items,
                trackingMode,
                entitySetName,
                entityChangedCallback,
                collectionChangedCallback
            )
        {
            this.context = context;
            name = OpenDataRegistry.MappedNames[typeof(TEntity)];
            if (name != null)
                _query = context.CreateQuery<TEntity>(typeof(TEntity).Name);

        }

        public virtual DataServiceQuery<TEntity> Query => _query;

        public virtual IEnumerable<TEntity> Load()
        {
            Load(_query);
            return this;
        }

        public virtual void Load(int offset = 0, int limit = 0)
        {
            var q = limit > 0 ? _query.Skip(offset).Take(limit) : _query;
            Load(q);
        }

        public void LoadAsync(int offset = 0, int limit = 0)
        {
            var q = limit > 0 ? _query.Skip(offset).Take(limit) : _query;
            LoadAsync(q);
        }

        public virtual IQueryable<TEntity> Load(Func<IQueryable<TEntity>, IQueryable<TEntity>> query)
        {
            Load(query(_query));
            return query(this.AsQueryable());
        }

        public virtual IQueryable<TEntity> Load(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
                Load(_query.Where(predicate));
            return this.AsQueryable().Where(predicate);
        }

        public virtual void Load<TOrigin>(
            TOrigin origin,
            Func<TOrigin, Expression<Func<TEntity, bool>>> predicate
        )
        {
            this.origin = origin;
            if (predicate != null)
                Load(predicate.Invoke(origin));
        }

        public virtual void LoadAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query)
        {
            LoadAsync(query(_query));
        }

        public virtual void LoadAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
                LoadAsync(_query.Where(predicate));
        }

        public virtual void LoadAsync<TOrigin>(
            TOrigin origin,
            Func<TOrigin, Expression<Func<TEntity, bool>>> predicate
        )
        {
            this.origin = origin;
            if (predicate != null)
                LoadAsync(predicate.Invoke(origin));
        }

        public virtual void Save()
        {
            context.SaveChanges(SaveChangesOptions.BatchWithIndependentOperations);
        }

        public virtual async Task SaveAsync()
        {
            await context.SaveChangesAsync(SaveChangesOptions.BatchWithIndependentOperations);
        }

        public virtual void AddRange(IEnumerable<TEntity> items)
        {
            items.ForEach(e => Add(e));
        }

        protected override void InsertItem(int index, TEntity item)
        {
            base.InsertItem(index, item);
            _listing.Put(item.Id, this[index]);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            _listing.RemoveAt(index);
        }

        protected override void SetItem(int index, TEntity item)
        {
            base.SetItem(index, item);
            _listing.Set(item.Id, this[index]);

        }

        protected override void ClearItems()
        {
            base.ClearItems();
            _listing.Clear();
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);
            var olditem = _listing[oldIndex];
            _listing.RemoveAt(oldIndex);
            _listing[newIndex] = olditem;
        }

        public bool ContainsKey(object key)
        {
            return _listing.ContainsKey(key);
        }

        public bool TryGet(object key, out TEntity value)
        {
            return _listing.TryGet(key, out value);
        }

        public string KeyString(params object[] keys)
        {
            return $"{typeof(TEntity).Name}({(keys.Length > 1 ? keys.Aggregate(string.Empty, (a, b) => $"{a},{b}") : keys[0])})";
        }

        public string KeyStringOnly(params object[] keys)
        {
            return $"{typeof(TEntity).Name}({(keys.Length > 1 ? keys.Aggregate(string.Empty, (a, b) => $"{a},{b}") : keys[0])})";
        }
    }
}
