namespace Undersoft.SDK.Benchmarks.Updating.Models.Agreements
{
    public class AgreementType : Identifiable
    {
        public int CurrentVersion { get; set; } = 232432423;
        public string Name { get; set; } = "hshdkjsadkhak";
        public bool IsGiodoType { get; set; } = true;

        public AgreementText AgreementText { get; set; } = new AgreementText();
    }

    public class EmptyAgreementType : Identifiable
    {
        public int CurrentVersion { get; set; }
        public string Name { get; set; }
        public bool IsGiodoType { get; set; }

        public EmptyAgreementText AgreementText { get; set; } = new EmptyAgreementText();
    }
}
