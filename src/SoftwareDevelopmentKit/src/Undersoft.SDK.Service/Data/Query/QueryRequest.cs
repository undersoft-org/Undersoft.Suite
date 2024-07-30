using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Data.Query
{
    public class QueryRequest<T> : QueryRequest, IQueryRequest<T> where T : class, IInnerProxy
    {
        public QueryRequest() { }

        public QueryRequest(IQueryParameters<T> parameters) : base(parameters) { }

        public QueryRequest(T data) : base(data) { }

        public new IQueryParameters<T> Parameters { get => (IQueryParameters<T>)(base._parameters ??= new QueryParameters<T>()); set => base._parameters = value; }

        public new T Data { get => (T)(base.Data ??= typeof(T).New<T>()); set => base.Data = value; }
    }

    public class QueryRequest : DataObject, IQueryRequest
    {
        protected IQueryParameters _parameters;

        public QueryRequest() { }

        public QueryRequest(IQueryParameters parameters) { _parameters = parameters; }

        public QueryRequest(object data) { Data = data; }

        public IQueryParameters Parameters { get => _parameters ??= new QueryParameters(); set => _parameters = value; }

        public object Data { get; set; }
    }
}