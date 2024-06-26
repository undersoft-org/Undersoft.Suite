using Microsoft.OData.Edm;

// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Clients
{
    /// <summary>
    /// The event client.
    /// </summary>
    public class EventClient : OpenDataClient<IEventStore>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventClient"/> class.
        /// </summary>
        /// <param name="serviceUri">The service uri.</param>
        public EventClient(Uri serviceUri) : base(serviceUri) { }

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
}