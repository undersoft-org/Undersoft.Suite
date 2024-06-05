namespace System.IO
{
    public static class StreamExtensions
    {
        public static byte[] GetAllBytes(this Stream stream)
        {
            var memoryStream = new MemoryStream();
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }
            stream.CopyTo(memoryStream);
            if (memoryStream.TryGetBuffer(out ArraySegment<byte> buffer))
                return buffer.Array;
            return memoryStream.ToArray();
        }

        public static async Task<byte[]> GetAllBytesAsync(
            this Stream stream,
            CancellationToken cancellationToken = default
        )
        {
            var memoryStream = new MemoryStream();
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }
            await stream.CopyToAsync(memoryStream, cancellationToken);
            if (memoryStream.TryGetBuffer(out ArraySegment<byte> buffer))
                return buffer.Array;
            return memoryStream.ToArray();
        }

        public static async Task CopyToAsync(
            this Stream stream,
            Stream destination,
            CancellationToken cancellationToken
        )
        {
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }
            await stream.CopyToAsync(destination);
        }
    }
}
