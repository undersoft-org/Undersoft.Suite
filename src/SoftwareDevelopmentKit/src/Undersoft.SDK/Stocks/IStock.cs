namespace Undersoft.SDK.Stocks
{
    public interface IStock
    {
        public object this[int index]
        {
            get;
            set;
        }
        public object this[int index, int field, Type type]
        {
            get;
            set;
        }

        void Open();
        void Close();
    }
}
