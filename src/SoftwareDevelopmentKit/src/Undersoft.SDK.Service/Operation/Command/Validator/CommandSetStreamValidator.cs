using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Operation.Command.Validator;



public class CommandSetStreamValidator<TDto> : CommandSetValidator<TDto> where TDto : class, IOrigin, IInnerProxy
{
    public CommandSetStreamValidator(IServicer servicer) : base(servicer) { }
}