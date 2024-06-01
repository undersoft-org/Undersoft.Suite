using System.ComponentModel;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK
{
    public interface IIdentifiable
    {
        long Id { get; set; }
        long TypeId { get; set; }
    }
}
