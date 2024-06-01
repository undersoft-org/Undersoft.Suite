using Microsoft.AspNetCore.Http;
using Undersoft.SDK.Service.Server.Resource.Container;

namespace Undersoft.SDK.Service.Server.Resource
{
    public class ResourceFile : ResourceFileContainer, IFormFile, IResourceFile
    {
        private IFormFile _formFile;
        private Stream _stream;

        public ResourceFile(ResourceFileContainer container, string filename) : base(container.ContainerName)
        {
            var task = GetOrNullAsync(filename);
            task.Wait();
            _stream = task.Result;
            _formFile = new FormFile(_stream, 0, _stream.Length, filename.Split('.')[0], filename);
        }
        public ResourceFile(string containerName, string filename) : base(containerName)
        {
            var task = GetOrNullAsync(filename);
            task.Wait();
            _stream = task.Result;
            _formFile = new FormFile(_stream, 0, _stream.Length, filename.Split('.')[0], filename);
        }
        public ResourceFile(string path) : base(Path.GetDirectoryName(path))
        {
            var filename = Path.GetFileName(path);
            var task = GetOrNullAsync(filename);
            task.Wait();
            _stream = task.Result;
            _formFile = new FormFile(_stream, 0, _stream.Length, filename.Split('.')[0], filename);
        }

        public virtual string ContentType => _formFile.ContentType;

        public virtual string ContentDisposition => _formFile.ContentDisposition;

        public IHeaderDictionary Headers => _formFile.Headers;

        public virtual long Length => _formFile.Length;

        public virtual string Name => _formFile.Name;

        public virtual string FileName => _formFile.FileName;

        public virtual void CopyTo(Stream target)
        {
            _formFile.CopyTo(target);
        }

        public virtual Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            return _formFile.CopyToAsync(target, cancellationToken);
        }

        public virtual Stream OpenReadStream()
        {
            return _formFile.OpenReadStream();
        }

  
    }
}
