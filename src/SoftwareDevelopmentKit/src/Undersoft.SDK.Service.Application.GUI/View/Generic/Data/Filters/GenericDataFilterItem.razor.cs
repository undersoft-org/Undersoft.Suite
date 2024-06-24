using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Filters
{
    public partial class GenericDataFilterItem : ViewItem
    {
        private Type _type = default!;
        private int _index;
        private string? _name { get; set; } = "";
        private string? _label { get; set; }
        private List<Option<string>>? _operandOptions { get; set; }
        private List<Option<string>>? _linkOptions { get; set; }

        private string? _operandValue
        {
            get => Filter.Operand.ToString();
            set
            {
                if (Enum.TryParse<CompareOperand>(value, true, out var result))
                    Filter.Operand = result;
            }
        }
        private List<Option<string>>? OperandOptions => _operandOptions ??=
            Enum.GetNames<CompareOperand>()
                .ForEach(n => new Option<string> { Value = n, Text = n })
                .ToList();

        private string? _linkdValue
        {
            get => Filter.Link.ToString();
            set
            {
                if (Enum.TryParse<LinkOperand>(value, true, out var result))
                    Filter.Link = result;
            }
        }
        private List<Option<string>>? LinkOptions => _linkOptions ??=
          Enum.GetNames<LinkOperand>()
              .ForEach(n => new Option<string> { Value = n, Text = n })
              .ToList();

        [CascadingParameter]
        public bool IsAddable { get; set; } = false;

        [CascadingParameter]
        private bool IsOpen { get; set; }

        [CascadingParameter]
        public bool ShowIcons { get; set; } = true;

        [Parameter]
        public Filter Filter { get; set; } = default!;

        [Parameter]
        public override int Index { get => base.Index; set => base.Index = value; }

        [Parameter]
        public bool AutoFocus { get; set; }

        protected override void OnInitialized()
        {
            _type = FilteredType;
            _name = Rubric.RubricName;
            _label = (Rubric.DisplayName != null) ? Rubric.DisplayName : Rubric.RubricName;

            Id = Filter.Id;
            TypeId = Filter.TypeId;

            base.OnInitialized();
        }

        public override object? Value
        {
            get => Filter.Value;
            set => Filter.Value = value;
        }

        [CascadingParameter]
        public Type FilteredType { get; set; } = default!;

        [CascadingParameter]
        public override IViewData Data
        {
            get => base.Data;
            set => base.Data = value;
        }

        [CascadingParameter]
        public override IViewItem? Root
        {
            get => base.Root;
            set => base.Root = value;
        }

        private void OnValueChanged(object value)
        {
            if (Parent != null)
            {
                ((IViewFilter)Parent).UpdateFilters();
                Parent.RenderView();
            }
        }

        private event EventHandler<object> _onMenuItemChange = default!;

        [CascadingParameter]
        public EventHandler<object> OnMenuItemChange
        {
            get => _onMenuItemChange;
            set
            {
                if (value != null)
                    _onMenuItemChange += value;
            }
        }
    }
}
