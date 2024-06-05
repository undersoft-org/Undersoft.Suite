using Undersoft.SDK.Instant.Series;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series.Base;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewRubrics : ListingBase<ViewRubric>, IViewRubrics
{
    public IInstantSeries? Series { get; set; }

    public IViewRubrics? KeyRubrics { get; set; }

    public IQueryParameters GetQuery(Func<IViewRubric, bool>? predicate = null)
    {
        var query = new QueryParameters();
        IEnumerable<IViewRubric> rubrics = this;
        if (predicate != null)
            rubrics = this.Where(predicate);

        rubrics.ForEach(r =>
        {
            if (r.Sorted)
                query.Sorters.Add(new Sort((MemberRubric)r, r.SortBy));

            if (r.Filtered)
                query.Filters.Add(r.Filters);
        });

        return query;
    }
}

