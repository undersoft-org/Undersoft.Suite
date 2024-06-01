namespace Undersoft.SDK.Service
{
    public class ServiceObject<T> : ServiceObject, IServiceObject<T> where T : class
    {
        public new T Value { get; set; }

        public ServiceObject()
        {
        }

        public ServiceObject(T obj)
        {
            Value = obj;
        }
    }

    public class ServiceObject : IServiceObject
    {
        public object Value { get; set; }

        public ServiceObject()
        {
        }

        public ServiceObject(object obj)
        {
            Value = obj;
        }
    }
}