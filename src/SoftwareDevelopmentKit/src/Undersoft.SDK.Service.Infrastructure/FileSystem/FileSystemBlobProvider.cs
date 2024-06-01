using Polly;
using System;
using System.IO;
using System.Threading.Tasks;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Infrastructure.FileSystem;

public class FileSystemBlobProvider : BlobProviderBase
{
    protected IBlobFilePathCalculator FilePathCalculator { get; }

    public FileSystemBlobProvider(IBlobFilePathCalculator filePathCalculator)
    {
        FilePathCalculator = filePathCalculator;
    }

    public override async Task SaveAsync(BlobProviderSaveArgs args)
    {
        var filePath = FilePathCalculator.Calculate(args);

        if (!args.OverrideExisting && await ExistsAsync(filePath))
        {
            throw new BlobAlreadyExistsException($"Saving BLOB '{args.BlobName}' does already exists in the container '{args.ContainerName}'! EntitySet {nameof(args.OverrideExisting)} if it should be overwritten.");
        }

        var dirname = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dirname))
            Directory.CreateDirectory(dirname);

        var fileMode = args.OverrideExisting
            ? FileMode.Create
            : FileMode.CreateNew;

        await Policy.Handle<IOException>()
            .WaitAndRetryAsync(2, retryCount => TimeSpan.FromSeconds(retryCount))
            .ExecuteAsync(async () =>
            {
                using (var fileStream = File.Open(filePath, fileMode, FileAccess.Write))
                {
                    await args.BlobStream.CopyToAsync(
                        fileStream,
                        args.CancellationToken
                    );

                    await fileStream.FlushAsync();
                }
            });
    }

    public override Task<bool> DeleteAsync(BlobProviderArgs args)
    {
        var filePath = FilePathCalculator.Calculate(args);
        return Task.Run(() =>
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        });
    }

    public override Task<bool> ExistsAsync(BlobProviderArgs args)
    {
        var filePath = FilePathCalculator.Calculate(args);
        return ExistsAsync(filePath);
    }

    public override async Task<Stream> GetOrNullAsync(BlobProviderArgs args)
    {
        var filePath = FilePathCalculator.Calculate(args);

        if (!File.Exists(filePath))
        {
            return null;
        }

        return await Policy.Handle<IOException>()
            .WaitAndRetryAsync(2, retryCount => TimeSpan.FromSeconds(retryCount))
            .ExecuteAsync(async () =>
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    return await TryCopyToMemoryStreamAsync(fileStream, args.CancellationToken);
                }
            });
    }

    protected virtual Task<bool> ExistsAsync(string filePath)
    {
        return Task.FromResult(File.Exists(filePath));
    }
}