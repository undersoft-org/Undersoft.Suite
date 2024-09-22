using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data
{
    public partial class GenericDataStore<TStore, TDto, TModel> : ViewStore<TStore, TDto, TModel>, IViewProgress where TStore : IDataServiceStore where TDto : class, IOrigin, IInnerProxy where TModel : class, IOrigin, IInnerProxy
    {
        [Inject]
        public override IServicer? Servicer { get; set; }

        [Inject]
        public override IAccessContext? Access { get; set; }

        [Inject]
        public override IDialogService? DialogService { get; set; }
    }
}
