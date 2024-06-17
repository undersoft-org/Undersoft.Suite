﻿using Microsoft.OData.Edm;

// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************


namespace Undersoft.SVC.Service.Clients;

/// <summary>
/// The service client.
/// </summary>
public class VaccinationClient : OpenDataClient<IVaccinationStore>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceClient"/> class.
    /// </summary>
    /// <param name="serviceUri">The service uri.</param>
    public VaccinationClient(Uri serviceUri) : base(serviceUri) { }

    /// <summary>
    /// On model creating.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>An <see cref="IEdmModel"/></returns>
    protected override IEdmModel OnModelCreating(IEdmModel builder)
    {

        return base.OnModelCreating(builder);
    }
}
