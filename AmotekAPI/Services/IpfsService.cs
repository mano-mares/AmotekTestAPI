using Ipfs.Api;

namespace AmotekAPI.Services
{
    public class IpfsService
    {
        private readonly IpfsClient _ipfsClient;

        public IpfsService()
        {
            _ipfsClient = new IpfsClient();
        }

        public async Task<string> UploadImageAsync(Stream imageStream)
        {
            var result = await _ipfsClient.FileSystem.AddAsync(imageStream);
            return result.Id.Hash.ToString();
        }

        public async Task<Stream> DownloadImageAsync(string imageHash)
        {
            var result = await _ipfsClient.FileSystem.ReadFileAsync(imageHash);
            return result;
        }
    }
}
