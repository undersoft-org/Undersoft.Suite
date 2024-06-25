using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public interface IGenericDataGrid
    {
        bool Checked { get; set; }
        IViewDataStore DataStore { get; set; }
        bool Editable { get; set; }
        bool Expandable { get; set; }
        Func<string, string> ForRowClass { get; set; }
        Func<string, string> ForRowStyle { get; set; }
        string? GridTemplateColumns { get; set; }
        bool Multiline { get; set; }
        bool Multiselect { get; set; }
        bool Resizable { get; set; }
        string? RowStyle { get; set; }
        int RubricOrdinalSeed { get; set; }
        bool Showable { get; set; }
        bool ShowHover { get; set; }
    }
}