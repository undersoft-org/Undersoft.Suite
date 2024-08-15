using Undersoft.SDK.Tests.Mocks.Models.Origins;

namespace Undersoft.SDK.Tests.Mocks.Models.Agreements
{
    public class AgreementType : Identifiable
    {
        public int CurrentVersion { get; set; }
        public string Name { get; set; }
        public bool IsGiodoType { get; set; }

        public virtual Agreements Agreements { get; set; } = new Agreements();
        public virtual AgreementVersions Versions { get; set; } = new AgreementVersions();
        public virtual OriginAgreementTypes Origins { get; set; } = new OriginAgreementTypes();
    }
}
