using System.Collections;

namespace Undersoft.SDK.Series
{
    public interface IFindable<V> : IFindable, IEnumerable<V>, IList<V>
    {
        new V this[object key] { get; set; }
    }

    public interface IFindable : IEnumerable
    {
        object this[object key] { get; set; }
    }
}
