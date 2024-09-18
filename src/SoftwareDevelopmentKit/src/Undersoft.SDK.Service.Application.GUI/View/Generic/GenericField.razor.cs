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
        protected bool _nullable = false;
        private IProxy _proxy = default!;
        protected int _index;
        protected int _size;
        protected string? _name { get; set; } = "";
        protected string? _label { get; set; } = default!;
        protected bool _required { get; set; }
        protected InputMode _inputMode { get; set; } = InputMode.None;
        protected TextFieldType _textFieldType { get; set; } = TextFieldType.Text;
        protected bool _upload { get; set; }
        protected bool _identity { get; set; }
        protected bool _disbled { get; set; }
        protected string _placeholder = default!;

        private long? _longValue
        {
            get => _nullable ? (long?)Value : (long)Value!;
            set => Value = value != null ? value : Value;
        }
        private double? _doubleValue
        {
            get => _nullable ? (double?)Value : (double)Value!;
            set => Value = value != null ? value.Value : Value;
        }
        private int? _intValue
        {
            get => _nullable ? (int?)Value : (int)Value!;
            set => Value = value != null ? value : Value;
        }
        private float? _floatValue
        {
            get => _nullable ? (float?)Value : (float)Value!;
            set => Value = value != null ? value : Value;
        }
        private decimal? _decimalValue
        {
            get => _nullable ? (decimal?)Value : (decimal)Value!;
            set => Value = value != null ? value : Value;
        }
        private string? _identityValue
        {
            get => Value?.ToString();
            set => Value = value != null ? Int64.Parse(value) : Value;
        }
        private string? _textValue
        {
            get => Value?.ToString();
            set => Value = value;
        }
        private DateTime? _dateValue
        {
            get => _nullable ? (DateTime?)Value : (DateTime)Value!;
            set => Value = value != null ? value : Value;
        }
        private DateTime? _dateOnlyValue
        {
            get => Value != null ? _nullable ? ((DateOnly?)Value).Value.ToDateTime(new TimeOnly()) : ((DateOnly)Value).ToDateTime(new TimeOnly()) : null;
            set => Value = value != null ? new DateOnly(value.Value.Year, value.Value.Month, value.Value.Day) : Value;
        }
        private DateTime? _timeOnlyValue
        {
            get => Value != null ? _nullable ? new DateTime(((TimeOnly?)Value).Value.Ticks) : new DateTime(((TimeOnly)Value).Ticks) : null;
            set => Value = value != null ? new TimeOnly(value.Value.Ticks) : Value;
        }
        private DateTime? _dateOffsetValue
        {
            get => Value != null ? _nullable ? ((DateTimeOffset?)Value).Value.DateTime : ((DateTimeOffset)Value).DateTime : null;
            set => Value = value != null ? new DateTimeOffset(value.Value) : Value;
        }
        private DateTime? _timeSpan
        {
            get => Value != null ? _nullable ? new DateTime(((TimeSpan?)Value).Value.Ticks) : new DateTime(((TimeSpan)Value).Ticks) : null;
            set => Value = value != null ? new TimeSpan(value.Value.Ticks) : Value;
        }
        private bool _bitValue
        {
            get => Value is bool ? (bool)Value : false;
            set => Value = value;
        }
        private string? _enumValue
        {
            get => (Value != null && _type.IsAssignableTo(typeof(Enum))) ? Value.ToString() : null;
            set
            {
                if (Enum.TryParse(_type, value, true, out var result))
                    Value = result;
            }
        }
        private List<Option<string>>? _enumOptions =>
            (Value != null && _type.IsAssignableTo(typeof(Enum)))
                ? Enum.GetNames(_type)
                    .ForEach(n => new Option<string> { Value = n, Text = n })
                    .ToList()
                : null;

        protected override void OnInitialized()
        {
            if (Data != null)
            {
                _proxy = Data.Model.Proxy;
                TypeId = Data.Model.TypeId;
                if (Rubric != null)
                    Id = Data.Model.Id.UniqueKey(Rubric.Id);
                else
                    Id = Data.Model.Id;
            }
            if (Rubric != null)
            {
                var targetType = Rubric.RubricType;
                _nullable = targetType.IsNullable();
                if (_nullable)
                    _type = targetType.GetNotNullableType();
                else
                    _type = targetType;
                _upload = Rubric.IsFile;
                _identity = Rubric.IsIdentity;
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
            }
            base.OnInitialized();
        }

        [Parameter]
        public bool AutoFocus { get; set; } = false;

        [Parameter]
        public FluentInputAppearance Appearance { get; set; } = FluentInputAppearance.Outline;

        [Parameter]
        public override string? Label
        {
            get => _label;
            set { _label = value; }
        }

        [Parameter]
        public virtual string Width { get; set; } = "200px";

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
                if (_proxy[_index] != value)
                {
                    _proxy[_index] = value;
                    if (ValueChanged.HasDelegate)
                        ValueChanged.InvokeAsync(value);
                }
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
