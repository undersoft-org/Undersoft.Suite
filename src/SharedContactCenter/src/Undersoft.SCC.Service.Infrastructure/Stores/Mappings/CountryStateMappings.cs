using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Infrastructure
// ********************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities.Contacts;
    using Undersoft.SCC.Domain.Entities.Countries;

    public class CountryStateMappings : EntityTypeMapping<CountryState>
    {
        const string TABLE_NAME = "CountryStates";

        public override void Configure(EntityTypeBuilder<CountryState> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder.RelateOneToSet<CountryState, ContactAddress>
                (c => c.CountryState,
                c => c.Addresses,
                ExpandSite.OnLeft,
                true);

        }
    }
}
