namespace Undersoft.SDK.Serialization;

public interface IDocumentSerializable<D>
{
    D Document { get; set; }

    string TypeName { get; set; }

    T GetStructure<T>();

    void SetDocument<T>(T structure);
}