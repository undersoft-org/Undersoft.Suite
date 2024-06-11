namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{

    public interface IViewFilter : IViewItem, IViewLoadable
    {
        bool IsOpen { get; set; }

        void UpdateFilters();

        void ClearFilters();

        Task ApplyFiltersAsync();
    }
}