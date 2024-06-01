using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewValidator
    {
        Task<ValidationResult> ValidateAsync();
        Task<ValidationResult> ValidateAsync(string propertyName);
        Task<ValidationResult> ValidateAsync(IViewData subContent, string? subRubricName = null);
    }
}