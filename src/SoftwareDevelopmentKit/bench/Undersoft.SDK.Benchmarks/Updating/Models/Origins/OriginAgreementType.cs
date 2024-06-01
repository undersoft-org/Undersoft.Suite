using System.Collections.ObjectModel;
using Undersoft.SDK.Benchmarks.Updating.Models.Agreements;

namespace Undersoft.SDK.Benchmarks.Updating.Models.Origins
{
    public class OriginAgreementType : Identifiable
    {
        public long TypeId { get; set; }

        public virtual AgreementType Type { get; set; }
    }

    public class OriginAgreementTypes : KeyedCollection<long, OriginAgreementType>
    {
        protected override long GetKeyForItem(OriginAgreementType item)
        {
            return item.Id == 0 ? item.AutoId() : item.Id;
        }
    }
}
