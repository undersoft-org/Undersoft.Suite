namespace Undersoft.SDK.Tests.Mocks.Models.Agreements
{
    public class AgreementFormat : Identifiable
    {
        public string Name { get; set; }

        public virtual Agreements Agreements { get; } = new Agreements();
    }


}
