using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Series
{
    public interface IListing<TUnique> : IReadOnlyList<TUnique>, IList<TUnique> where TUnique : IIdentifiable
    {
        object this[object key] { get; set; }

        TUnique Single { get; }
    }
}