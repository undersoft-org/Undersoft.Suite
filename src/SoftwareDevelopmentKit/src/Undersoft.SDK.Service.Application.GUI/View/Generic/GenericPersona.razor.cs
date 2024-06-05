using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic;

public partial class GenericPersona<TModel> : ViewItem<TModel> where TModel : class, IOrigin, IInnerProxy
{
    [Parameter]
    public string Image { get; set; } = default!;

    [Parameter]
    public string? Name { get; set; } = null;

    [Parameter]
    public string ImageSize { get; set; } = default!;

    [Parameter]
    public string Initials { get; set; } = default!;

    [Parameter]
    public bool ShowName { get; set; } = false;

    [Parameter]
    public Func<TModel, string> ForFirstName { get; set; } = default!;

    [Parameter]
    public Func<TModel, string> ForLastName { get; set; } = default!;

    [Parameter]
    public Func<TModel, string> ForImage { get; set; } = default!;

    [Parameter]
    public Func<TModel, byte[]> ForImageData { get; set; } = default!;

    protected override void OnInitialized()
    {
        if (Name == null)
        {
            if (ForFirstName != null)
                Name += ForFirstName(Content.Model);
            if (ForLastName != null)
            {
                var last = ForLastName(Content.Model);
                if (last != null)
                {
                    if (Name != null)
                        Name += " " + last;
                    else
                        Name = last;
                }
            }
        }

        if (ForImage != null && ForImageData != null)
        {
            var image = ForImage(Content.Model);
            var imageData = ForImageData(Content.Model);
            if (image != null && imageData != null)
            {
                Image = imageData.ToDataUri(image.Split(";")[0]);
            }
        }

        if (Image == null)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var names = Name.Split(' ');
                if (!string.IsNullOrEmpty(names[0]))
                    Initials += names[0].First().ToString().ToUpper();
                if (names.Length > 1)
                    if (!string.IsNullOrEmpty(names[1]))
                        Initials += names[1].First().ToString().ToUpper();
            }
            else
            {
                Image = new Icons.Regular.Size32.Person().ToDataUri(size: "25px", color: "white");
            }
        }
    }
}
