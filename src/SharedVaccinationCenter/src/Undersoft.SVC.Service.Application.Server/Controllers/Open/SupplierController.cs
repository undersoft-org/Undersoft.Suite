// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Open;

using Undersoft.SDK.Service.Server.Controller.Open;
using Undersoft.SVC.Service.Clients;
using Undersoft.SVC.Service.Contracts;

public class SupplierController
    : OpenDataRemoteController<
        long,
        ICatalogsStore,
        Supplier,
        Supplier,
        ServiceManager
    >
{
    public SupplierController(IServicer servicer) : base(servicer) { }
}
