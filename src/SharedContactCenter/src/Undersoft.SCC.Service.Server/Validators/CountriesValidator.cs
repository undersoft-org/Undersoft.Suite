using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK;
using Undersoft.SDK.Service.Operation.Command.Validator;

namespace Undersoft.SCC.Service.Server.Validators;

public class CountriesValidator : CommandSetValidator<Country>
{
    public CountriesValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateRequired(p => p.Contract.Name!);
            }
        );
    }
}
