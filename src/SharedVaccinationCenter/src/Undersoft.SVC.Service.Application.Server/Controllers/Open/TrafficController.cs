﻿// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Open;

using Undersoft.SDK.Service.Server.Controller.Open;
using Undersoft.SVC.Service.Contracts;

public class TrafficController
    : OpenDataRemoteController<
        long,
        IDataStore,
        Traffic,
        Traffic,
        ServiceManager
    >
{
    public TrafficController(IServicer servicer) : base(servicer) { }
}