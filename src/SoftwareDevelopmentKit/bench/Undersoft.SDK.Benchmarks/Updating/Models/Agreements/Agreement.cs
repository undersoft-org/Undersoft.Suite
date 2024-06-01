using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Series;

namespace Undersoft.SDK.Benchmarks.Updating.Models.Agreements
{
    public class Agreement : InnerProxy
    {
        [DisplayRubric("Agreement kind")]
        public AgreementKind Kind { get; set; } = AgreementKind.Agree;
        [IdentityRubric]
        [RequiredRubric]
        public Guid UserId { get; set; } = Guid.NewGuid();
        [RequiredRubric]
        [DisplayRubric("Version number")]
        public long VersionId { get; set; }
        [RequiredRubric]
        public long FormatId { get; set; }
        [RequiredRubric]
        public string Language { get; set; } = "pl";
        [VisibleRubric]
        public string Comments { get; set; } = "Comments";
        [DisplayRubric("Email address")]
        public string Email { get; set; } = "fdfds";
        [DisplayRubric("Phone number")]
        public string PhoneNumber { get; set; } = "3453453";

        public virtual IEnumerable<AgreementFormat> Formats { get; set; } = new Listing<AgreementFormat>(new[] { new AgreementFormat() { Id = 10 } });

        public virtual IEnumerable<AgreementVersion> Versions { get; set; } = new Listing<AgreementVersion>(new[] { new AgreementVersion() { Id = 10 }, new AgreementVersion() { Id = 20 } });

        public virtual AgreementType Type { get; set; } = new AgreementType();
    }

    public class EmptyAgreement : InnerProxy
    {
        [DisplayRubric("Agreement kind")]
        public AgreementKind Kind { get; set; }
        [IdentityRubric]
        [RequiredRubric]
        public Guid UserId { get; set; }
        [RequiredRubric]
        [DisplayRubric("Version number")]
        public long VersionId { get; set; }
        [RequiredRubric]
        public long FormatId { get; set; }
        [RequiredRubric]
        public string Language { get; set; }
        [VisibleRubric]
        public string Comments { get; set; }
        [DisplayRubric("Email address")]
        public string Email { get; set; }
        [DisplayRubric("Phone number")]
        public string PhoneNumber { get; set; }

        public virtual IEnumerable<EmptyAgreementFormat> Formats { get; set; } = new Listing<EmptyAgreementFormat>(new[] { new EmptyAgreementFormat() { Id = 10 } });

        public virtual IEnumerable<EmptyAgreementVersion> Versions { get; set; } = new Listing<EmptyAgreementVersion>(new[] { new EmptyAgreementVersion() { Id = 10 } });

        public virtual EmptyAgreementType Type { get; set; } = new EmptyAgreementType();
    }


    public class Agreements : KeyedCollection<long, Agreement>
    {
        protected override long GetKeyForItem(Agreement item)
        {
            return item.Id == 0 ? item.AutoId() : item.Id;
        }
    }
}
