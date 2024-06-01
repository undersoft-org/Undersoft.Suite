using FluentValidation.Results;

namespace Undersoft.SDK.Service.Operation.Remote.Command;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;

public interface IRemoteCommandSet<TModel> : IRemoteCommandSet where TModel : class, IOrigin, IInnerProxy
{
    public new IEnumerable<RemoteCommand<TModel>> Commands { get; }
}

public interface IRemoteCommandSet : IOperation
{
    public IEnumerable<IRemoteCommand> Commands { get; }

    public ValidationResult Result { get; set; }
}
