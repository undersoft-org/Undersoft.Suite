using System.Text.Json;

namespace Undersoft.SDK.Serialization;

public interface ISerializableJsonDocument
{
    JsonDocument Document { get; set; }

    string TypeName { get; set; }

    T GetDetail<T>();

    void SetGeneral<T>(T structure);
}
