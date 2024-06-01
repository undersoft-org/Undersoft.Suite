using Microsoft.OData.Client;
using System.Text.Json;

namespace Undersoft.SDK.Service.Data.Remote.Repository;

public partial class RemoteRepository<TEntity>
{
    public async Task<TModel> Setup<TModel>(string method, TModel arguments)
    {
        return await InvokeAsync("Setup", method, arguments);
    }

    public async Task<TModel> Access<TModel>(string method, TModel arguments)
    {
        return await InvokeAsync("Access", method, arguments);
    }

    public async Task<TModel> Action<TModel>(string method, TModel arguments)
    {
        return await InvokeAsync("Action", method, arguments);
    }

    private async Task<TModel> InvokeAsync<TModel>(string action, string method, TModel args)
    {
        object _args = args;
        var _isRelay = typeof(TModel) == typeof(Arguments);

        if (!_isRelay)
            _args = new Arguments(method, args);

        var preresponse =
            await RemoteContext.ExecuteAsync<byte[]>(
                new Uri($"{RemoteContext.BaseUri.OriginalString}/{Name}/{action}"),
                "POST", true,
                new BodyOperationParameter(method, _args)
            );
        var response = preresponse.FirstOrDefault()?.FromJson<Arguments>();
        if (response != null && response.Any())
        {
            object result = _isRelay
                ? response
                : response.FirstOrDefault().Deserialize();

            return (TModel)result;
        }
        return default;
    }

    private async Task<TModel> InvokeBatchAsync<TModel>(
        string action,
        IDictionary<string, TModel> batch
    ) where TModel : class
    {
        var _isRelay = batch.Any(d => d.Value.GetType() == typeof(Arguments));
        BodyOperationParameter[] _params = null;

        if (!_isRelay)
            _params = batch
                .ForEach(a => new BodyOperationParameter(a.Key, new Arguments(a.Key, a.Value)))
                .Commit();
        else
            _params = batch.ForEach(a => new BodyOperationParameter(a.Key, a.Value)).Commit();

        var response = (
            await RemoteContext.ExecuteAsync<Arguments>(
                new Uri($"{RemoteContext.BaseUri.OriginalString}/{Name}/{action}"),
                "POST",
                _params
            )
        ).FirstOrDefault();

        return _isRelay
           ? (TModel)(object)response
           : response.FirstOrDefault()?.Deserialize<TModel>();
    }
}
