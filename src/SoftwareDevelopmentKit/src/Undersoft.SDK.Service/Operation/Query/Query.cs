using MediatR;

namespace Undersoft.SDK.Service.Operation.Query;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Query;

public class Query<TEntity, TDto> : QueryBase, IRequest<Query<TEntity, TDto>>, IQuery<TEntity, TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
{
    public Query() : base() { }

    public Query(OperationType type) : base(type) { }

    public Query(OperationType type, object[] keys) : base(type, keys) { }

    public Query(OperationType type, IQueryParameters<TEntity> parameters) : base(type, parameters) { }

    public new IQueryParameters<TEntity> Parameters
    {
        get => (IQueryParameters<TEntity>)(_parameters ??= new QueryParameters<TEntity>());
        set => _parameters = value;
    }

    public override int Offset { get => this.Parameters.Offset; set => this.Parameters.Offset = value; }

    public override int Limit { get => this.Parameters.Limit; set => this.Parameters.Limit = value; }

    public override int Count { get => this.Parameters.Count; set => this.Parameters.Count = value; }

    public IAsyncEnumerable<TDto> AsyncResult { get; set; }

    public bool IsAsyncResult => AsyncResult != null;

    public new IQueryable<TDto> Result { get; set; }

    public new TDto SingleResult { get; set; }

    public override object Input => Parameters;

    public override object Output =>
        IsValid
            ? IsSingle
                ? SingleResult
                : IsAsyncResult
                    ? (object)AsyncResult
                    : (object)Result
            : (object)ErrorMessages;

    public new TEntity Data { get => this.Parameters.Data; set => this.Parameters.Data = value; }
}
