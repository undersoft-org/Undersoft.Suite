// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SDK
// *************************************************


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
