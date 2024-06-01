using System.Collections.ObjectModel;

namespace Undersoft.SDK.Benchmarks.Updating.Models.Agreements
{
    public class AgreementText : Identifiable
    {
        public ulong VersionId { get; set; } = 342343;
        public int VersionNumber { get; set; } = 10;
        public string Language { get; set; } = "PL";
        public string Content { get; set; } = "dlkfhshdfkhskjdhfkjhsdkjhfkjsdkjfhskdjhfkjshdkjfhskjdhfkjsdjkfhsjkdh";
    }

    public class EmptyAgreementText : Identifiable
    {
        public ulong VersionId { get; set; }
        public int VersionNumber { get; set; }
        public string Language { get; set; }
        public string Content { get; set; }
    }


    public class AgreementContents : Collection<AgreementText>
    {
    }
}