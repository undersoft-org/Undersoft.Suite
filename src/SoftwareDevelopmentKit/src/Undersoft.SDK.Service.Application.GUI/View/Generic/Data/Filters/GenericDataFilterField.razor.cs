using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Filters
{
    public partial class GenericDataFilterField : GenericField
    {
        protected override void OnInitialized()
        {
            _nullable = FilteredType.IsNullable();
            _type = FilteredType.GetNotNullableType();
            _size = Rubric.RubricSize;
            _name = Filter.Member;
            _identity = Rubric.IsIdentity;

            if (_type.IsValueType || Rubric.FilterMembers!.Length < 2)
                _label = null;
            else
                _label = Filter.Member.Split('.').Last();

            if (Rubric.Width != null)
                Width = Rubric.Width;
            if (Rubric.Height != null)
                Height = Rubric.Height;

            Id = Filter.Id;
            TypeId = Filter.TypeId;
        }

        [CascadingParameter]
        public bool IsAddable { get; set; } = false;

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
                if (Filter.Value != value)
                {
                    Filter.Value = value;
                    if (ValueChanged.HasDelegate)
                        ValueChanged.InvokeAsync(value);
                }
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
