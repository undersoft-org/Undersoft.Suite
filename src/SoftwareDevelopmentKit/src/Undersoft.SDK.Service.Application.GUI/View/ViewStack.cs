using Microsoft.FluentUI.AspNetCore.Components;

namespace Undersoft.SDK.Service.Application.GUI.View
{
    public class ViewStack : Attribute
    {
        public ViewStack() { }

        public Orientation? Orientation { get; set; }


        public HorizontalAlignment? HorizontalAlignment { get; set; }


        public VerticalAlignment? VerticalAlignment { get; set; }


        public int? VerticalGap { get; set; }


        public int? HorizontalGap { get; set; }
    }
}
