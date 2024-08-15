using System.Collections.ObjectModel;
using Undersoft.SDK.Tests.Mocks.Models.Agreements;

namespace Undersoft.SDK.Tests.Mocks.Models.Origins
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
