using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic
{
    public partial class GenericField : ViewItem, IIdentifiable, IViewItem
    {
        protected Type _type = default!;
        private IProxy _proxy = default!;
        protected int _index;
        protected int _size;
        protected string? _name { get; set; } = "";
        protected string? _label { get; set; } = default!;
        protected bool _required { get; set; }
        protected InputMode _inputMode { get; set; } = InputMode.None;
        protected TextFieldType _textFieldType { get; set; } = TextFieldType.Text;
        protected bool _isUpload { get; set; }
        protected string _placeholder = default;

        private long? _longValue
        {
            get => Value is long ? (long?)Value : null;
            set => Value = value;
        }
        private double? _doubleValue
        {
            get => Value is double ? (long?)Value : null;
            set { Value = value; }
        }
        private int? _intValue
        {
            get => Value is int ? (int?)Value : null;
            set => Value = value;
        }
        private float? _floatValue
        {
            get => Value is float ? (float?)Value : null;
            set { Value = value; }
        }
        private decimal? _decimalValue
        {
            get => Value is decimal ? (decimal?)Value : null;
            set { Value = value; }
        }
        private string? _textValue
        {
            get => Value is string ? (string?)Value : null;
            set { Value = value; }
        }
        private DateTime? _timeValue
        {
            get => Value is DateTime ? (DateTime?)Value : null;
            set { Value = value; }
        }
        private bool _bitValue
        {
            get => Value is bool ? (bool)Value : false;
            set => Value = value;
        }
        private string? _enumValue
        {
            get => Value is Enum ? Value.ToString() : null;
            set
            {
                if (Enum.TryParse(_type, value, true, out var result))
                    Value = result;
            }
        }
        private List<Option<string>>? _enumOptions =>
            Value is Enum
                ? Enum.GetNames(_type)
                    .ForEach(n => new Option<string> { Value = n, Text = n })
                    .ToList()
                : null;


        protected override void OnInitialized()
        {
            _type = Rubric.RubricType;
            if (Data != null)
                _proxy = Data.Model.Proxy;
            _isUpload = Rubric.IsFile;
            _index = Rubric.RubricId;
            _size = Rubric.RubricSize;
            _name = Rubric.RubricName;
            _label = Rubric.DisplayName != null ? Rubric.DisplayName : Rubric.RubricName;
            _placeholder = _label;
            _required = Rubric.Required;
            _inputMode = GetInputMode();
            _textFieldType = GetTexType();

            if (Rubric.Width != null)
                Width = Rubric.Width;
            if (Rubric.Height != null)
                Height = Rubric.Height;

            Id = Model.Id.UniqueKey(Rubric.Id);
            TypeId = Model.TypeId;

            base.OnInitialized();
        }

        [Parameter]
        public bool AutoFocus { get; set; }

        [Parameter]
        public override string? Label
        {
            get => _label;
            set { _label = value; }
        }

        [Parameter]
        public virtual string Width { get; set; } = "190px";

        [Parameter]
        public virtual string Height { get; set; } = "28px";

        [CascadingParameter]
        public override IViewData Data { get; set; } = default!;

        [CascadingParameter]
        protected EditContext FormContext { get; set; } = default!;

        public override object? Value
        {
            get { return _proxy[_index]; }
            set
            {
                _proxy[_index] = value;
                if (ValueChanged.HasDelegate)
                    ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<object?> ValueChanged { get; set; }

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

        protected bool TryParseValueFromString(
            string? value,
            [MaybeNullWhen(false)] out object result,
            [NotNullWhen(false)] out string? validationErrorMessage
        )
        {
            result = null;
            validationErrorMessage = "";
            if (_type.IsPrimitive)
            {
                if (_type.IsAssignableTo(typeof(long)))
                {
                    if (long.TryParse(value, out long _result))
                        result = _result;
                }
                if (_type.IsAssignableTo(typeof(decimal)))
                {
                    if (decimal.TryParse(value, out decimal _result))
                        result = _result;
                }
                else if (_type.IsAssignableTo(typeof(DateTime)))
                {
                    if (DateTime.TryParse(value, out DateTime _result))
                        result = _result;
                }
                else if (_type.IsAssignableTo(typeof(bool)))
                {
                    if (bool.TryParse(value, out bool _result))
                        result = _result;
                }
                else if (_type.IsAssignableTo(typeof(Enum)))
                {
                    Enum.TryParse(_type, value, out result);
                }
            }
            else if (_type == typeof(string) || _type.IsAssignableTo(typeof(IFormattable)))
            {
                result = value;
            }
            if (result != null)
                return false;
            validationErrorMessage = "Unable to parse value";
            result = "";
            return true;
        }

        public bool? GetBool()
        {
            return (bool?)Value;
        }

        public bool? SetBool()
        {
            return (bool?)Value;
        }
    }
}
