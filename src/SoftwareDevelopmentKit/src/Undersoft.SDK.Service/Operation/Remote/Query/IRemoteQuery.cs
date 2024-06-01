using FluentValidation.Results;
using Undersoft.SDK.Service.Data.Query;


namespace Undersoft.SDK.Service.Operation.Remote.Query;

public interface IRemoteQuery<TDto, TModel> : IRemoteQuery where TDto : class, IOrigin, IInnerProxy
{
    new IQueryParameters<TDto> Parameters { get; }

    IAsyncEnumerable<TModel> AsyncResult { get; set; }

    bool IsAsyncResult { get; }

    new TDto Data { get; set; }
}

public interface IRemoteQuery : IOperation
{
    int Count { get; set; }
    string ErrorMessages { get; }
    bool IsSingle { get; }
    bool IsValid { get; }
    object[] Keys { get; }
    int Limit { get; set; }
    IQueryParameters Parameters { get; set; }
    IQueryable Result { get; set; }
    IInnerProxy SingleResult { get; set; }
    ValidationResult ValidationResult { get; set; }
    object Data { get; set; }
}