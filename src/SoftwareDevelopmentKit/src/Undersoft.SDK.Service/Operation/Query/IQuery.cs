namespace Undersoft.SDK.Service.Operation.Query;

using FluentValidation.Results;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Query;

public interface IQuery<TEntity, TDto> : IQuery where TEntity : class, IOrigin, IInnerProxy
{
    new IQueryParameters<TEntity> Parameters { get; set; }

    IAsyncEnumerable<TDto> AsyncResult { get; set; }

    bool IsAsyncResult { get; }

    new TEntity Data { get; set; }
}

public interface IQuery : IOperation
{
    int Count { get; set; }
    string ErrorMessages { get; }
    bool IsSingle { get; }
    bool IsValid { get; }
    object[] Keys { get; }
    int Limit { get; set; }
    int Offset { get; set; }
    IQueryParameters Parameters { get; set; }
    IQueryable Result { get; set; }
    IInnerProxy SingleResult { get; set; }
    ValidationResult ValidationResult { get; set; }
    object Data { get; set; }
}