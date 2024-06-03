using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data
{
    public partial class GenericDataStore<TStore, TModel> : ViewStore<TStore, TModel> where TStore : IDataServiceStore where TModel : class, IOrigin, IInnerProxy
    {
        [Inject]
        public override IServicer Servicer { get; set; } = default!;

        [Inject]
        public override IDialogService DialogService { get; set; } = default!;
    }
}
