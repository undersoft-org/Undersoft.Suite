using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Validator
{
    public partial class GenericValidator<TValidator, TModel> : FluentComponentBase, IViewValidator
        where TValidator : class, IValidator<IViewData<TModel>>
        where TModel : class, IOrigin, IInnerProxy
    {
        [CascadingParameter]
        private EditContext FormContext { get; set; } = default!;

        [CascadingParameter]
        private IViewData<TModel> Content { get; set; } = default!;

        private ValidationMessageStore ValidationMessageStore = default!;

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            EditContext previousEditContext = FormContext;

            var previousContent = Content;

            await base.SetParametersAsync(parameters);

            if (previousContent != Content)
                Content.Validator = this;

            if (FormContext != previousEditContext)
                FormContextChanged();
        }

        void FormContextChanged()
        {
            ValidationMessageStore = new ValidationMessageStore(FormContext);

            FormContext.OnFieldChanged += FieldChanged;
        }

        private ViewRubric? RecursiveFindViewRubric(IViewData content, string? rubricName)
        {
            if (rubricName == null)
                return null;

            var rubric = content.Rubrics[rubricName];

            if (rubric != null)
                return rubric;

            rubric = content.ExtendedRubrics[rubricName];
            if (rubric != null)
                return rubric;

            foreach (var r in content.Rubrics.Concat(content.ExtendedRubrics))
            {
                if (r.ViewItem != null)
                {
                    rubric = RecursiveFindViewRubric(r.ViewItem.Data, rubricName);
                    if (rubric != null)
                        break;
                }
            }

            return rubric;
        }

        public async Task<ValidationResult> ValidateAsync()
        {
            ValidationMessageStore.Clear();
            Content.ClearErrors();

            var context = new ValidationContext<IViewData<TModel>>(Content);
            var result = await Validator.ValidateAsync(context);

            if (result.Errors.Any())
            {
                result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ForEach(r =>
                    {
                        var rubric = RecursiveFindViewRubric(
                            Content,
                            r.Key.Split(".").LastOrDefault()
                        );

                        if (rubric != null)
                        {
                            rubric.Errors.Clear();
                            r.ForEach(e =>
                            {
                                if (
                                    rubric.Errors.TryAdd(e.ErrorMessage)
                                    && rubric.FieldIdentifier.Model != null
                                )
                                    ValidationMessageStore.Add(
                                        rubric.FieldIdentifier,
                                        e.ErrorMessage
                                    );

                                Content.Notes.Errors =
                                    Content.Notes.Errors == null
                                        ? e.ErrorMessage
                                        : string.Concat(
                                            Content.Notes.Errors,
                                            ", ",
                                            e.ErrorMessage
                                        );
                            });
                        }
                    });
            }
            Content.RenderView();
            FormContext.NotifyValidationStateChanged();
            return result;
        }

        public async Task<ValidationResult> ValidateAsync(IViewData subContent, string? subRubricName = null)
        {
            ValidationMessageStore.Clear();
            Content.ClearErrors();

            var context = new ValidationContext<IViewData<TModel>>(Content);
            var result = await Validator.ValidateAsync(context);

            if (result.Errors.Any())
            {
                result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ForEach(r =>
                    {
                        bool invalid = false;
                        if (subRubricName != null)
                        {
                            var splits = r.Key.Split(".");
                            var item = splits[splits.Length - 2];
                            if (subRubricName != item)
                                invalid = true;
                        }
                        if (!invalid)
                        {

                            var rubric = RecursiveFindViewRubric(
                                subContent,
                                r.Key.Split(".").LastOrDefault()
                            );

                            if (rubric != null)
                            {
                                rubric.Errors.Clear();
                                r.ForEach(e =>
                                {
                                    if (
                                        rubric.Errors.TryAdd(e.ErrorMessage)
                                        && rubric.FieldIdentifier.Model != null
                                    )
                                        ValidationMessageStore.Add(
                                            rubric.FieldIdentifier,
                                            e.ErrorMessage
                                        );

                                    Content.Notes.Errors =
                                        Content.Notes.Errors == null
                                            ? e.ErrorMessage
                                            : string.Concat(
                                                Content.Notes.Errors,
                                                ", ",
                                                e.ErrorMessage
                                            );
                                });
                            }
                            else
                            {
                                r.ForEach(e =>
                                {
                                    result.Errors.Remove(e);
                                });
                            }
                        }
                    });
            }
            if (string.IsNullOrEmpty(Content.Notes.Errors))
                result.Errors.Clear();

            Content.RenderView();
            FormContext.NotifyValidationStateChanged();
            return result;
        }

        public async Task<ValidationResult> ValidateAsync(string propertyName)
        {
            IViewRubric rubric = Content.Rubrics[propertyName];

            ValidationMessageStore.Clear(rubric.FieldIdentifier);
            rubric.Errors.Clear();

            var context = new ValidationContext<IViewData<TModel>>(Content);
            var result = await Validator.ValidateAsync(context);
            var _result = new ValidationResult(
                result.Errors.Where(
                    e => rubric.RubricName.Equals(e.PropertyName.Split(".").LastOrDefault())
                )
            );

            if (_result.Errors.Any())
            {
                _result.Errors.ForEach(e =>
                {
                    if (rubric.Errors.TryAdd(e.ErrorMessage))
                        ValidationMessageStore.Add(rubric.FieldIdentifier, e.ErrorMessage);
                });
            }
            rubric.ViewItem.RenderView();
            FormContext.NotifyValidationStateChanged();
            return _result;
        }

        async void FieldChanged(object? sender, FieldChangedEventArgs args)
        {
            ValidationMessageStore.Clear(args.FieldIdentifier);

            if (args.FieldIdentifier.Model.GetType().IsAssignableTo(typeof(IViewItem)))
            {
                var field = (IViewItem)args.FieldIdentifier.Model;
                IViewRubric rubric = field.Rubric;
                rubric.Errors.Clear();

                var context = new ValidationContext<IViewData<TModel>>(Content);
                var result = await Validator.ValidateAsync(context);
                var _result = new ValidationResult(result.Errors.Where(e =>
                {
                    if (Content.ActiveRubric == null)
                        return rubric.RubricName.Equals(e.PropertyName.Split(".").LastOrDefault());

                    var activeName = Content.ActiveRubric.RubricName;
                    var splits = e.PropertyName.Split(".");
                    var item = splits[splits.Length - 2];

                    return item == activeName && rubric.RubricName.Equals(e.PropertyName.Split(".").LastOrDefault());
                }));

                if (_result.Errors.Any())
                {
                    _result.Errors.ForEach(e =>
                    {
                        if (rubric.Errors.TryAdd(e.ErrorMessage))
                            ValidationMessageStore.Add(args.FieldIdentifier, e.ErrorMessage);
                    });
                }
                if (Content.View != null)
                    Content.View.RenderView();

                field.RenderView();

                FormContext.NotifyValidationStateChanged();
            }
        }
    }
}
