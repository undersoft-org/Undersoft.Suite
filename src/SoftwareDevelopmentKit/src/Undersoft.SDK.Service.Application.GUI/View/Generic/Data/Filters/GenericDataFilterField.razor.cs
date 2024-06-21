using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.Extensions;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Filters
{
    public partial class GenericDataFilterField : GenericField
    {
        protected override void OnInitialized()
        {
            _type = FilteredType.GetNotNullableType();
            _size = Rubric.RubricSize;
            _label = Filter.Member.Split('.').Last();
            if (NoLabel || _label == Rubric.RubricName)
                _label = null;

            if (Rubric.Width != null)
                Width = Rubric.Width;
            if (Rubric.Height != null)
                Height = Rubric.Height;

            Id = Filter.Id;
            TypeId = Filter.TypeId;
        }

        [CascadingParameter]
        public bool NoLabel { get; set; }

        [Parameter]
        public Filter Filter { get; set; } = default!;

        [CascadingParameter]
        public Type FilteredType { get; set; } = default!;

        [Parameter]
        public override string? Label
        {
            get => _label;
            set { _label = value; }
        }

        [Parameter]
        public override IViewData Data { get; set; } = default!;

        public override object? Value
        {
            get => Filter.Value;
            set
            {
                Filter.Value = value;
                if (ValueChanged.HasDelegate)
                    ValueChanged.InvokeAsync(value);
            }
        }

        private InputMode GetInputMode()
        {
            if (_name != null)
            {
                if (_name.ToLower().Contains("phone"))
                    return InputMode.Telephone;
                if (_name.ToLower().Contains("search"))
                    return InputMode.Search;
                if (_name.ToLower().Contains("url"))
                    return InputMode.Url;
                if (_type.IsAssignableTo(typeof(decimal)))
                    return InputMode.Decimal;
            }
            return InputMode.Text;
        }

        private TextFieldType GetTexType()
        {
            if (_name != null)
            {
                if (_name.ToLower().Contains("passw"))
                    return TextFieldType.Password;
                if (_name.ToLower().Contains("email"))
                    return TextFieldType.Email;
                if (_name.ToLower().Contains("phone"))
                    return TextFieldType.Tel;
                if (_name.ToLower().Contains("url"))
                    return TextFieldType.Url;
            }
            return TextFieldType.Text;
        }

    }
}
