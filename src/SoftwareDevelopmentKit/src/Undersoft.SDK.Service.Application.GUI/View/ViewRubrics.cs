using Undersoft.SDK.Instant.Series;
using Undersoft.SDK.Series.Base;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewRubrics : ListingBase<ViewRubric>, IViewRubrics
{
    public IInstantSeries? Series { get; set; }

    public IViewRubrics? KeyRubrics { get; set; }
}

