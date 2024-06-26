// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

namespace Undersoft.SVC.Service.Application.Server.Controllers.Open;

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Server.Controller.Open;
using Undersoft.SVC.Service.Clients.Abstractions;
using Undersoft.SVC.Service.Contracts.Catalogs;



/// <summary>
/// The contact controller.
/// </summary>
public class OfficeController
    : OpenDataRemoteController<long, ICatalogsStore, Office,
        Office,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PatientNodeController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public OfficeController(IServicer servicer) : base(servicer) { }
}