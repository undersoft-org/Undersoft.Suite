// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Open;

using Undersoft.SVC.Service.Clients;
using Undersoft.SVC.Service.Contracts;

public class RequestController
    : OpenDataRemoteController<long, IInventoryStore, Request, Request, ServiceManager>
{
    public RequestController(IServicer servicer) : base(servicer) { }
}
