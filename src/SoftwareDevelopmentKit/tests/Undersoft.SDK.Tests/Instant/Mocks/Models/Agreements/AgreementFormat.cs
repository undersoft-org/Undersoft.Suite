namespace Undersoft.SDK.Tests.Instant
{
    public class AgreementFormat : Identifiable
    {
        public string Name { get; set; }

        public virtual Agreements Agreements { get; } = new Agreements();
    }


}
