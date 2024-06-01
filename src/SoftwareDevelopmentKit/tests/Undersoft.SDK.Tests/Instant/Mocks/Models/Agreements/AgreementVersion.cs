using System.Collections.ObjectModel;
using Undersoft.SDK.Series;

namespace Undersoft.SDK.Tests.Instant
{
    public class AgreementVersion : Identifiable
    {

        public int VersionNumber { get; set; }

        public int OriginId { get; set; }

        public virtual AgreementType Type { get; set; }
        public virtual Listing<Agreement> Agreements { get; set; }
        public virtual Listing<AgreementText> Texts { get; set; }
    }

    public class AgreementVersions : KeyedCollection<long, AgreementVersion>
    {
        protected override long GetKeyForItem(AgreementVersion item)
        {
            return (item.Id == 0) ? (long)item.AutoId() : item.Id;
        }
    }
}
