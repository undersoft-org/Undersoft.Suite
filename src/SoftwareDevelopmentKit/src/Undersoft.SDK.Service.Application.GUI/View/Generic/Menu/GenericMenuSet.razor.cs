using FluentValidation;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Menu
{
    public partial class GenericMenuSet<TMenu> : ViewItem<TMenu> where TMenu : class, IOrigin, IInnerProxy
    {
        public bool IsSingle => Data.Rubrics.Count < 2;

        [Parameter]
        public NavigationManager? NavigationManager { get; set; }

        [Parameter]
        public bool ShowLabels { get; set; } = false;

        [Parameter]
        public bool ShowIcons { get; set; } = true;

        private event EventHandler<object> _onMenuItemChange = default!;

        [Parameter]
        public virtual EventHandler<object> OnMenuItemChange { get => _onMenuItemChange; set { if (value != null) _onMenuItemChange += value; } }

        public override string ViewId => $"{Model.Id.ToString()}{Rubric.Id.ToString()}";

        protected override void OnParametersSet()
        {
            if (Data == null)
                Data = new ViewData<TMenu>(typeof(TMenu).New<TMenu>());

            Data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
            Data.ExtendedRubrics.ForEach(r =>
                {
                    Model.Proxy[r.RubricId] = r.RubricType.New();
                    if (r.DisplayName == null)
                        r.DisplayName = r.RubricName;
                });
            Data.Put(Data.ExtendedRubrics.ForEach(r => typeof(ViewData<>).MakeGenericType(r.RubricType).New<IViewData>(Model.Proxy[r.RubricId])));

            base.OnParametersSet();
        }
    }
}