using System.Collections.ObjectModel;

namespace Undersoft.SDK.Tests.Instant
{
    public class AgreementText : Identifiable
    {
        public ulong VersionId { get; set; }
        public int VersionNumber { get; set; }
        public string Language { get; set; }
        public string Content { get; set; }
        public virtual AgreementVersion Version { get; set; }
    }

    public class AgreementContents : Collection<AgreementText>
    {
    }
}