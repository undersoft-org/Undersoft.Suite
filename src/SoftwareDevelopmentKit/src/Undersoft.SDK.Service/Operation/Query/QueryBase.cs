namespace Undersoft.SDK.Service.Operation.Query;

using FluentValidation.Results;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Query;

public abstract class QueryBase : IQuery
{
    protected IQueryParameters _parameters;

    public QueryBase()
    {
        ValidationResult = new ValidationResult();
    }

    public QueryBase(OperationType type) : this()
    {
        OperationType = type;
    }

    public QueryBase(OperationType type, object[] keys) : this(type)
    {
        Keys = keys;
    }

    public QueryBase(OperationType type, IQueryParameters parameters) : this(type)
    {
        _parameters = parameters;
    }

    public virtual int Offset { get => Parameters.Offset; set => Parameters.Offset = value; }

    public virtual int Limit { get => Parameters.Limit; set => Parameters.Limit = value; }

    public virtual int Count { get => Parameters.Count; set => Parameters.Count = value; }

    public virtual ValidationResult ValidationResult { get; set; }

    public bool IsValid => ValidationResult.IsValid;

    public string ErrorMessages => ValidationResult.ToString();

    public bool IsSingle => SingleResult != null;

    public virtual IQueryParameters Parameters { get => _parameters ??= new QueryParameters(); set => _parameters = value; }

    public virtual IQueryable Result { get; set; }

    public virtual IInnerProxy SingleResult { get; set; }

    public virtual object[] Keys { get; set; }

    public virtual object Input => Parameters;

    public virtual object Output => IsValid ? IsSingle ? SingleResult : (object)Result : (object)ErrorMessages;

    public OperationType OperationType { get; set; }

    public virtual object Data { get => Parameters.Data; set => Parameters.Data = value; }
}
