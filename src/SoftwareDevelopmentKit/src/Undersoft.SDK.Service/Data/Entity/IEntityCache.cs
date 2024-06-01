using System;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Data.Entity;

public interface IEntityCache<TStore, TEntity> : IStoreCache<TStore>
{
}