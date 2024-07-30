using System.Text.Json;

namespace Undersoft.SDK.Service.Data.Object.Detail
{
    public interface IDetail : IDataObject
    {
        JsonDocument Document { get; set; }
        string Name { get; set; }

        T GetStructure<T>();
        object GetStructure();

        void SetDocument<T>(T structure);
        void SetDocument(object structure);
    }
}