// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Open;

using Undersoft.SDK.Service;
using Undersoft.SVC.Service.Contracts;

public class CampaignController
    : OpenDataRemoteController<long, IDataStore, Campaign, Campaign, ServiceManager>
{
    public CampaignController(IServicer servicer) : base(servicer) { }
}
