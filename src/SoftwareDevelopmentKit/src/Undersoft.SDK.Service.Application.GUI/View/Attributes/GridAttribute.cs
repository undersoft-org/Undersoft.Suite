using Microsoft.FluentUI.AspNetCore.Components;

namespace Undersoft.SDK.Service.Application.GUI.View.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class GridAttribute : Attribute
    {
        public GridAttribute() { }

        public int? xs { get; set; }


        public int? sm { get; set; }


        public int? md { get; set; }


        public int? lg { get; set; }


        public int? xl { get; set; }

        public int? xxl { get; set; }


        public JustifyContent? Justify { get; set; }

        public int? Spacing { get; set; }

        public string? Style { get; set; }

        public string? Gap { get; set; }


        public GridItemHidden? HiddenWhen { get; set; }
    }
}
