using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SSC.Service.Application.GUI.Compound.Landing;

public class LandingTopMenu : DataObject
{
    [MenuGroup]
    [Extended]
    [IconRubric("MenuIcon")]
    public LandingTopMenuItems Menu { get; set; } = new LandingTopMenuItems();

    public Icon MenuIcon = new Icons.Regular.Size20.Navigation();
}

