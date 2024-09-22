namespace Undersoft.SDK.Ethernet.Transfer
{
    using System.IO;

    public interface ITransferable
    {
        int InputChunks { get; set; }

        int ItemsCount { get; }

        int CurrentChunk { get; set; }

        int OutputChunks { get; set; }

        object GetHeader();

        object[] GetMessage();

        object Deserialize(ITransferBuffer buffer);

        object Deserialize(Stream stream);

        int Serialize(
            ITransferBuffer buffer,
            int offset,
            int batchSize
        );

        int Serialize(
            Stream stream,
            int offset,
            int batchSize
        );
    }
}
