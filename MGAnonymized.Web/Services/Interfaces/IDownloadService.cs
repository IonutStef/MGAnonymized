using System.Threading.Tasks;

namespace MGAnonymized.Web.Services.Interfaces
{
    public interface IDownloadService
    {
        Task<T> ReadJsonToObject<T>(string url) where T : class;

        Task SaveFile(string imageUrl, string localImagePath);
    }
}