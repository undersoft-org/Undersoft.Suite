using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK;
using Undersoft.SDK.Service.Operation.Command.Validator;

namespace Undersoft.SCC.Service.Server.Validators;

public class CountryValidator : CommandValidator<Country>
{
    public CountryValidator(IServicer servicer) : base(servicer)
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
