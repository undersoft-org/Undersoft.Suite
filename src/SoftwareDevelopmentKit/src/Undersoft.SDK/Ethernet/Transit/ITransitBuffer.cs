namespace Undersoft.SDK.Ethernet
{
    public interface ITransitBuffer : IDisposable
    {
        int Offset { get; set; }

        long Size { get; set; }

        byte[] Input { get; }

        int InputId { get; set; }

        IntPtr InputPtr { get; }

        byte[] Output { get; set; }

        int OutputId { get; set; }

        IntPtr OutputPtr { get; }

        EthernetSite Site { get; }
    }
}
