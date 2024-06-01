using MediatR;

namespace Undersoft.SDK.Service.Operation.Invocation;

using Undersoft.SDK;
using Undersoft.SDK.Uniques;

public class Invocation<TDto> : InvocationBase, IRequest<Invocation<TDto>> where TDto : class
{
    private Uscn code = new Uscn(Unique.NewId);

    public new TDto Return { get; set; }

    protected Invocation() { }

    protected Invocation(OperationType commandMode, Type serviceType, string method, object argument)
        : base(commandMode, serviceType, method, argument) { }

    protected Invocation(OperationType commandMode, Type serviceType, string method, Arguments arguemnts)
        : base(commandMode, serviceType, method, arguemnts) { }

    protected Invocation(OperationType commandMode, Type serviceType, string method, object[] arguments)
       : base(commandMode, serviceType, method, arguments) { }

    protected Invocation(OperationType commandMode, Type serviceType, string method, byte[] binaries)
      : base(commandMode, serviceType, method, binaries) { }

    public int CompareTo(IUnique other)
    {
        return code.CompareTo(other);
    }

    public override long Id
    {
        get => code.Id;
        set => code.Id = value;
    }

    public long TypeId
    {
        get => code.TypeId;
        set => code.TypeId = value;
    }
}
