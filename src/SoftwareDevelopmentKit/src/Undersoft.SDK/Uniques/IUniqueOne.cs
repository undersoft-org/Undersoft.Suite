using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Uniques
{
    public interface IUniqueOne<T>
    {
        IQueryable<T> Queryable { get; }
    }
}
