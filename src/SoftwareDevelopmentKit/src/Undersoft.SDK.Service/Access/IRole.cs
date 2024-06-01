using Undersoft.SDK.Series;

namespace Undersoft.SDK.Service.Access
{
    public interface IRole
    {
        string Name { get; set; }
        string NormalizedName { get; set; }
    }
}