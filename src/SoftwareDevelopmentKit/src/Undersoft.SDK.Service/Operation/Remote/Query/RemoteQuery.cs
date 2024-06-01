using MediatR;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Operation.Remote.Query;

public class RemoteQuery<TDto, TModel>
    : RemoteQueryBase,
        IRequest<RemoteQuery<TDto, TModel>>,
        IRemoteQuery<TDto, TModel>
    where TModel : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
{
    public RemoteQuery() : base() { }

    public RemoteQuery(OperationType type) : base(type) { }

    public RemoteQuery(OperationType type, object[] keys) : base(type, keys) { }

    public RemoteQuery(OperationType type, IQueryParameters<TDto> parameters)
        : base(type, parameters) { }

    public new IQueryParameters<TDto> Parameters
    {
        get => (IQueryParameters<TDto>)(_parameters ??= new QueryParameters<TDto>());
        set => _parameters = value;
    }

    public override int Offset
    {
        get => this.Parameters.Offset;
        set => this.Parameters.Offset = value;
    }

    public override int Limit
    {
        get => this.Parameters.Limit;
        set => this.Parameters.Limit = value;
    }

    public override int Count
    {
        get => this.Parameters.Count;
        set => this.Parameters.Count = value;
    }

    public IAsyncEnumerable<TModel> AsyncResult { get; set; }

    public bool IsAsyncResult => AsyncResult != null;

    public new IQueryable<TModel> Result { get; set; }

    public new TModel SingleResult { get; set; }

    public override object Input => Parameters;

    public override object Output =>
        IsValid
            ? IsSingle
                ? SingleResult
                : IsAsyncResult
                    ? (object)AsyncResult
                    : (object)Result
            : (object)ErrorMessages;

    public new TDto Data
    {
        get => this.Parameters.Data;
        set => this.Parameters.Data = value;
    }
}
