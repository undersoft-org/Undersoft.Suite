namespace Undersoft.SDK.Ethernet.Transfer
{
    public interface ITransferBuffer : IDisposable
    {
        int Offset { get; set; }
        long Size { get; set; }

        byte[] Input { get; }
        int InputId { get; set; }
        nint InputPtr { get; }

        byte[] Output { get; set; }
        int OutputId { get; set; }
        nint OutputPtr { get; }

        EthernetSite Site { get; }
    }
}
