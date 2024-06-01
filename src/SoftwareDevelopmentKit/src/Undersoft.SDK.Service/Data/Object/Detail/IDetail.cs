using System.Text.Json;

namespace Undersoft.SDK.Service.Data.Object.Detail
{
    public interface IDetail : IDataObject
    {
        JsonDocument Document { get; set; }
        string Name { get; set; }

        T GetObject<T>();
        object GetObject();

        void SetDocument<T>(T structure);
        void SetDocument(object structure);
    }
}