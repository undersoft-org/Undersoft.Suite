namespace Undersoft.SDK.Service.Data.Cache;

using Undersoft.SDK;

public interface IDataCache : ITypedSeries<IIdentifiable>
{
    ITypedSeries<IIdentifiable> CacheSet<T>() where T : IIdentifiable;

    T Lookup<T>(T item) where T : IIdentifiable;

    ISeries<IIdentifiable> Lookup<T>(Func<ITypedSeries<IIdentifiable>, ISeries<IIdentifiable>> selector) where T : IIdentifiable;

    T Lookup<T>(object key) where T : IIdentifiable;

    T Lookup<T>(params object[] key) where T : IIdentifiable;

    ISeries<IIdentifiable> Lookup<T>(Tuple<string, object> valueNamePair) where T : IIdentifiable;

    T Lookup<T>(T item, params string[] propertyNames) where T : IIdentifiable;

    T[] Lookup<T>(Func<ISeries<IIdentifiable>, IIdentifiable> key, params Func<ITypedSeries<IIdentifiable>, ISeries<IIdentifiable>>[] selectors)
        where T : IIdentifiable;

    T[] Lookup<T>(object[] key, params Tuple<string, object>[] valueNamePairs) where T : IIdentifiable;

    ISeries<IIdentifiable> Lookup<T>(object key, string propertyNames) where T : IIdentifiable;

    IEnumerable<T> Memorize<T>(IEnumerable<T> items) where T : IIdentifiable;

    T Memorize<T>(T item) where T : IIdentifiable;

    T Memorize<T>(T item, params string[] names) where T : IIdentifiable;

    Task<T> MemorizeAsync<T>(T item) where T : IIdentifiable;

    Task<T> MemorizeAsync<T>(T item, params string[] names) where T : IIdentifiable;

    ITypedSeries<IIdentifiable> Catalog { get; }
}