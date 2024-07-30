using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Uniques;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Menu
{
    public partial class GenericMenuItem : ViewItem
    {
        [CascadingParameter]
        private NavigationManager? NavigationManager { get; set; }

        private Type _type = default!;
        private IProxy _proxy = default!;
        private IProxy _childProxy = default!;
        private int _index;
        private string? _name { get; set; } = "";
        private string? _label { get; set; }

        [CascadingParameter]
        private bool IsOpen { get; set; }

        [CascadingParameter]
        public bool ShowIcons { get; set; } = true;

        protected override void OnInitialized()
        {
            _type = Rubric.RubricType;
            _proxy = Model.Proxy;
            _index = Rubric.RubricId;
            _name = Rubric.RubricName;
            _label = (Rubric.DisplayName != null) ? Rubric.DisplayName : Rubric.RubricName;

            if (Parent != null)
            {
                Id = Rubric.Id.UniqueKey(Parent.Id);
                TypeId = _type.UniqueKey(Parent.TypeId);
            }

            if (Rubric != null && Rubric.IsMenuGroup && _type.IsClass)
            {
                if (!Data.TryGet(Value, out IViewData expandData))
                {
                    expandData = typeof(ViewData<>).MakeGenericType(_type).New<IViewData>(Value);
                    expandData.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
                    Data.Put(expandData);
                }
                ExpandData = expandData;
            }
            base.OnInitialized();
        }

        public override object? Value
        {
            get { return _proxy[_index]; }
            set
            {
                _proxy[_index] = value;
            }
        }

        [CascadingParameter]
        public override IViewData Data { get => base.Data; set => base.Data = value; }

        [CascadingParameter]
        public override IViewItem? Root { get => base.Root; set => base.Root = value; }

        private event EventHandler<object> _onMenuItemChange = default!;

        [CascadingParameter]
        public EventHandler<object> OnMenuItemChange { get => _onMenuItemChange; set { if (value != null) _onMenuItemChange += value; } }

        public IViewData ExpandData { get; set; } = default!;

        public void OnClick()
        {
            if (Rubric.IsLink && NavigationManager != null)
            {
                if (Rubric.LinkValue != null)
                    NavigationManager.NavigateTo(Rubric.LinkValue);
                if (Rubric.RubricType == typeof(string))
                {
                    var uri = Value?.ToString();
                    if (uri != null)
                        NavigationManager.NavigateTo(uri);
                }
            }
            else if (Rubric.Invoker != null)
            {
                Rubric.Invoker.Invoke(Value);
            }
            else if (OnMenuItemChange != null)
            {
                OnMenuItemChange(this, Data);
            }
            else if (Rubric.Extended && Rubric.IsMenuGroup)
            {
                Data.ForOnly(d => d.StateFlags.Expanded, d => d.StateFlags.Expanded = false).Commit();
                ExpandData.StateFlags.Expanded = true;
            }
            IsOpen = false;
        }
    }
}
