namespace Undersoft.SDK.Rubrics.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FileRubricAttribute : RubricAttribute
    {
        public FileRubricType Type;

        public string DataMember;

        public FileRubricAttribute() { }

        public FileRubricAttribute(FileRubricType type, string dataMember = null)
        {
            Type = type;
            DataMember = dataMember;
        }
    }

    public enum FileRubricType
    {
        None,
        Path,
        Stream,
        Blob
    }
}
