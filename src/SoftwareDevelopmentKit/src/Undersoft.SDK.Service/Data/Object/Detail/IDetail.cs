using System.Text.Json;

namespace Undersoft.SDK.Service.Data.Object.Detail
{
    public interface IDetail : IDataObject
    {
        JsonDocument Document { get; set; }
        string Name { get; set; }

        T GetDetail<T>();
        object GetDetail();

        void SetGeneral<T>(T structure);
        void SetGeneral(object structure);
    }
}