namespace Undersoft.SDK.Uniques
{
    public interface IUnique<V> : IUnique
    {
        V UniqueValue { get; set; }

        //int[] UniqueOrdinals();

        //long CompactKey();

        //object[] UniqueValues();
    }
}
