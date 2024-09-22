namespace Undersoft.SDK.Ethernet.Transfer
{
    public interface ITransferObject
    {
        object Locate(object path = null);

        object Merge(object source, string name = null);
    }
}
