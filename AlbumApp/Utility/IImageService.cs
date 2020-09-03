using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AlbumApp.Utility
{
    public interface IImageService
    {
        Task<string> UploadPhoto(IFormFile photo);
        void CreateThumbnailImage(string photoName);
        bool ContentTypeValidation(string type);
        bool NumImagesValidation(int curentNumOfImages);
        Task<string> GetDescription(string fileName);
        string LargestImage(IEnumerable<string> fileNames);
        void DeletePhotos(string photoName);
    }
}