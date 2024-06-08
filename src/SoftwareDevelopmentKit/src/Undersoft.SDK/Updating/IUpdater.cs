using Undersoft.SDK.Instant;

namespace Undersoft.SDK.Updating;

using Proxies;
using Rubrics;

public interface IUpdater : IInstant
{
    IProxy Source { get; }

    IRubrics Rubrics { get; set; }

    object Clone();

    E Patch<E>();
    E Patch<E>(E item);

    E Put<E>();
    E Put<E>(E item);
}
