using System;
using System.Collections.Generic;

namespace Undersoft.SDK.Service.Data.Repository;

using Invoking;

public interface IRepositoryEvents
{
    IInvoker OnAdding { get; set; }
    IInvoker OnAddComplete { get; set; }
    IInvoker OnGetting { get; set; }
    IInvoker OnGetComplete { get; set; }
    IInvoker OnSetting { get; set; }
    IInvoker OnSetComplete { get; set; }
    IInvoker OnDeleting { get; set; }
    IInvoker OnDeleteComplete { get; set; }
    IInvoker OnSaving { get; set; }
    IInvoker OnSaveComplete { get; set; }
    IInvoker OnFiltering { get; set; }
    IInvoker OnFilterComplete { get; set; }
    IInvoker OnFinding { get; set; }
    IInvoker OnFindComplete { get; set; }
    IInvoker OnMapping { get; set; }
    IInvoker OnMapComplete { get; set; }
    IInvoker OnExist { get; set; }
    IInvoker OnExistComplete { get; set; }
    IInvoker OnNonExist { get; set; }
    IInvoker OnNonExistComplete { get; set; }
    IInvoker OnValidating { get; set; }
    IInvoker OnValidateComplete { get; set; }
}

