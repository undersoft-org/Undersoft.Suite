using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Data.Query
{
    public interface IQueryRequest<T> : IQueryRequest
    {
        new IQueryParameters<T> Parameters { get; set; }

        new T Data { get; set; }
    }

    public interface IQueryRequest : IDataObject
    {
        IQueryParameters Parameters { get; set; }

        public object Data { get; set; }
    }
}