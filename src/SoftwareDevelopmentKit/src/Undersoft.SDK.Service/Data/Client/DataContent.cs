using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Undersoft.SDK.Service.Data.Client
{
    public class DataContent : HttpContent
    {
        private byte[] buffer;

        public DataContent(DataArgument[] arguments)
        {
            buffer = arguments.Select(v => v.DataObject).ToArray().ToJsonBytes();
            SetContentType();
        }

        public DataContent(DataArgument argument)
        {
            buffer = argument.DataObject.ToJsonBytes();
            SetContentType();
        }

        public DataContent(Argument argument)
        {
            buffer = argument.Data;
            SetContentType();
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return stream.WriteAsync(buffer, 0, buffer.Length);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            if (buffer == null)
                return false;
            length = (long)buffer.Length;
            return true;
        }

        private void SetContentType()
        {
            Headers.ContentType = new MediaTypeHeaderValue(
                "application/json"
            );
        }
    }
}
