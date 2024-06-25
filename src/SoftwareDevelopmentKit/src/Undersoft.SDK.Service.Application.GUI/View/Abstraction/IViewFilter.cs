namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{

    public interface IViewFilter : IViewItem, IViewLoadable
    {
        bool IsOpen { get; set; }

        bool Added { get; }

        bool IsAddable { get; }

        void Close();

        void CloneLastFilter();

        void RemoveLastFilter();

        void UpdateFilters();

        void ClearFilters();

        Task ApplyFiltersAsync();
    }
}