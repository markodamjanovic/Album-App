using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace AlbumApp.Utility
{
    public class ImageService : IImageService
    {   
        const string IMAGES_FOLDER = "images";
        const string THUMBNAILS_FOLDER = "thumbnails";
        const int MAX_THUMB_HEIGHT = 120;
        const int MAX_THUMB_WIDTH = 120;
         List<string> allowedFileTypes = new List<string>()
        {
            "image/jpg",
            "image/jpeg",
            "image/png"
        };

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        private string CreateUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid().ToString()}_{fileName}";
        }

        private string GetUploadsFolder(string folder)
        {
            return Path.Combine(_webHostEnvironment.WebRootPath, folder);
        }

        private void ScaleImageDimensions(Image image, int maxWidth, int maxHeight, out int newWidth, out int newHeight)
        {
            float ratioX = (float)maxWidth / (float)image.Width;
            float ratioY = (float)maxHeight / (float)image.Height;
            float ratio = Math.Min(ratioX, ratioY);

            newWidth = (int)(image.Width * ratio);
            newHeight = (int)(image.Height * ratio);
        }

        public async Task<string> UploadPhoto(IFormFile photo)
        {   
            string uniqueFileName = CreateUniqueFileName(photo.FileName);
            string filePath = Path.Combine(GetUploadsFolder(IMAGES_FOLDER), uniqueFileName);
            await photo.CopyToAsync(new FileStream(Path.Combine(GetUploadsFolder(IMAGES_FOLDER), uniqueFileName), FileMode.Create));
            
            return uniqueFileName;
        }

        public void CreateThumbnailImage(string photoName)
        {   
            int thumbHeight;
            int thumbWidth;

            string filePath = Path.Combine(GetUploadsFolder(IMAGES_FOLDER), photoName);
            Image image = Image.FromFile(filePath);

            ScaleImageDimensions(image, MAX_THUMB_WIDTH, MAX_THUMB_HEIGHT, out thumbWidth, out thumbHeight);

            Image thumb = image.GetThumbnailImage(thumbWidth, thumbHeight, ()=>false, IntPtr.Zero);
            thumb.Save(Path.Combine(GetUploadsFolder(THUMBNAILS_FOLDER), photoName), image.RawFormat);        
        }

        public bool ContentTypeValidation(string type)
        {
            return allowedFileTypes.Contains(type);
        }
    }
}