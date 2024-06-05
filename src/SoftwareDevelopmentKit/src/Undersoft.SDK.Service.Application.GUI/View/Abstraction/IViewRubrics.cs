using Undersoft.SDK.Instant.Series;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction;

public interface IViewRubrics : ISeries<ViewRubric>
{
    IInstantSeries? Series { get; set; }

    IViewRubrics? KeyRubrics { get; set; }

    IQueryParameters GetQuery(Func<IViewRubric, bool>? predicate = null);
}

