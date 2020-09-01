using System.Net.Http;
using System.Threading.Tasks;

namespace AlbumApp.Utility
{
    public interface IComputerVisionService
    {
        Task<string> callAPI(byte[] image, string requestParameters);
    }
}