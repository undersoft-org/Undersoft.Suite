using System;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Data.Cache;
using Undersoft.SDK.Service.Data.Mapper;

namespace Undersoft.SDK.Service.Data.Store;

using Uniques;

public interface IStoreCache<TStore> : IDataCache
{
    IDataMapper Mapper { get; set; }
}