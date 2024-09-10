namespace System
{
    using System.IO;
    using Undersoft.SDK.Ethernet;

    public interface ITransitable
    {
        int InputChunks { get; set; }

        int ItemsCount { get; }

        int CurrentChunk { get; set; }

        int OutputChunks { get; set; }

        object Deserialize(ITransitBuffer buffer);

        object Deserialize(Stream stream);

        object GetHeader();

        object[] GetMessage();

        int Serialize(
            ITransitBuffer buffer,
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
