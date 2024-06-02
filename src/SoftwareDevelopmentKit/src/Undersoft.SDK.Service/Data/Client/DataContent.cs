using System.Net.Http.Headers;
using System.Text.Json;

namespace Undersoft.SDK.Service.Data.Client
{
    public class DataContent : ByteArrayContent
    {
        public DataContent(IEnumerable<DataArgument> arguments) : base(arguments.Select(a => a.DataObject).ToJsonBytes())
        {
            SetContentType();
        }

        public DataContent(DataArgument argument) : base(argument.DataObject.ToJsonBytes())
        {
            SetContentType();
        }

        public DataContent(Argument argument) : base(argument.Data)
        {
            SetContentType();
        }

        private void SetContentType()
        {
            Headers.ContentType = new MediaTypeHeaderValue(
                "application/json"
            );
        }
    }
}
