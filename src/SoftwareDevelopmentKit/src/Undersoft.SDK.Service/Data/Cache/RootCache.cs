using Microsoft.Extensions.Configuration;

namespace Undersoft.SDK.Service.Data.Cache;

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Object;

public static class RootCache
{
    static RootCache()
    {
        IConfigurationSection cacheconfig = ServiceManager
            .GetRootConfiguration()
            .DataCacheLifeTime();
        Catalog = new DataCache(
            TimeSpan.FromMinutes(
                cacheconfig.GetValue<uint>("Minutes") + cacheconfig.GetValue<uint>("Hours") * 60
            ),
            null,
            513
        );
    }

    public static async Task<T> Lookup<T>(object keys) where T : IIdentifiable
    {
        return await Task.Run((Func<T>)(() =>
        {
            if (Catalog.TryGet(keys, (long)DataObjectExtensions.GetDataTypeId(typeof(T)), out IIdentifiable output))
                return (T)output;
            return default;
        }));
    }

    public static T ToCache<T>(this T item) where T : IIdentifiable
    {
        return Catalog.Memorize(item);
    }

    public static T ToCache<T>(this T item, params string[] names) where T : IIdentifiable
    {
        return Catalog.Memorize(item, names);
    }

    public static async Task<T> ToCacheAsync<T>(this T item) where T : IIdentifiable
    {
        return await Task.Run(() => item.ToCache()).ConfigureAwait(false);
    }

    public static async Task<T> ToCacheAsync<T>(this T item, params string[] names)
        where T : IIdentifiable
    {
        return await Task.Run(() => item.ToCache(names)).ConfigureAwait(false);
    }

    public static IDataCache Catalog { get; }
}
