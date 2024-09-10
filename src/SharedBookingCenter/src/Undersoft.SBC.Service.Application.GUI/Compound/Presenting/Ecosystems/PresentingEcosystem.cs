using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SBC.Service.Application.GUI.Compound.Presenting.Ecosystems;

public class PresentingEcosystem : DataObject
{
    [MenuGroup]
    [Extended]
    public EcosystemCenter Center { get; set; } = new EcosystemCenter();
    public Icon CenterIcon = new Icons.Regular.Size20.BuildingSkyscraper();

    [MenuGroup]
    [Extended]
    public EcosystemService Service { get; set; } = new EcosystemService();
    public Icon ServiceIcon = new Icons.Regular.Size20.Server();

    [MenuGroup]
    [Extended]
    public EcosystemAplication Application { get; set; } = new EcosystemAplication();
    public Icon ApplicationIcon = new Icons.Regular.Size20.AppGeneric();
}

