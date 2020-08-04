using Microsoft.Extensions.Logging;
using MGAnonymized.Web.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MGAnonymized.Web.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly ILogger<DownloadService> _logger;
        public DownloadService(ILogger<DownloadService> logger)
        {
            _logger = logger;
        }

        public async Task<T> ReadJsonToObject<T>(string url) where T : class
        {
            using (var wc = new WebClient())
            {
                try
                { 
                    var json = await wc.DownloadStringTaskAsync(url);

                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch
                {
                    return default;
                }
            }
        }

        public async Task SaveFile(string fileUrl, string localFilePath)
        {
            using (var wc = new WebClient())
            {
                try
                {
                    _logger.LogInformation($"Downloading file: {fileUrl}.");

                    await wc.DownloadFileTaskAsync(new Uri(fileUrl), localFilePath);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error on downloading files.");
                }
            }
        }
    }
}