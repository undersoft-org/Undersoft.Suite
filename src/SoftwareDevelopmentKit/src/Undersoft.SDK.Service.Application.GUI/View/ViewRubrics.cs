using Undersoft.SDK.Instant.Series;
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
            {
                if (r.SortMembers == null)
                    r.SortMembers = new[] { r.RubricName };
                query.SortItems.Add(r.SortMembers.ForEach(m => new Sorter(m, r.SortBy)).Commit());
            }

            if (r.Filtered)
            {
                if (r.FilterMembers == null)
                    r.FilterMembers = new[] { r.RubricName };
                query.FilterItems.Add(r.FilterMembers.SelectMany(m => r.Filters.ForEach(f => new Filter(m, f.Value, f.Operand, f.Link))));
            }
        });

        return query;
    }
}

