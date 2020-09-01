using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AlbumApp.Utility
{
    public interface IImageService
    {
        Task<string> UploadPhoto(IFormFile photo);
        void CreateThumbnailImage(string photoName);
        bool ContentTypeValidation(string type);
    }
}