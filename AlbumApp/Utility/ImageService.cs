using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlbumApp.Utility
{
    public class ImageService : IImageService
    {   
        const string IMAGES_FOLDER = "images";
        const string THUMBNAILS_FOLDER = "thumbnails";
        const int MAX_THUMB_HEIGHT = 250;
        const int MAX_THUMB_WIDTH = 250;
         List<string> allowedFileTypes = new List<string>()
        {
            "image/jpg",
            "image/jpeg",
            "image/png"
        };

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IComputerVisionService _computerVision;

        public ImageService(IWebHostEnvironment webHostEnvironment, IComputerVisionService computerVision)
        {
            _webHostEnvironment = webHostEnvironment;
            _computerVision = computerVision;
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
            
            using(FileStream stream = new FileStream(Path.Combine(GetUploadsFolder(IMAGES_FOLDER), uniqueFileName), FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }
            
            return uniqueFileName;
        }
        
         public void CreateThumbnailImage(string photoName)
        {   
            int thumbHeight;
            int thumbWidth;

            string filePath = Path.Combine(GetUploadsFolder(IMAGES_FOLDER), photoName);
            
            //using(Image image = Image.FromStream(new MemoryStream(GetImageAsByteArray(filePath))))
            using(Image image = Image.FromStream(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                ScaleImageDimensions(image, MAX_THUMB_WIDTH, MAX_THUMB_HEIGHT, out thumbWidth, out thumbHeight);
                Image thumb = image.GetThumbnailImage(thumbWidth, thumbHeight, ()=>false, IntPtr.Zero);
                thumb.Save(Path.Combine(GetUploadsFolder(THUMBNAILS_FOLDER), photoName), image.RawFormat);        
            }
        }

        private byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

        public bool ContentTypeValidation(string type)
        {
            return allowedFileTypes.Contains(type);
        }

        public async Task<string> GetDescription(string fileName)
        {
            string filePath = Path.Combine(GetUploadsFolder(IMAGES_FOLDER), fileName);
            byte[] image = GetImageAsByteArray(filePath);
            
            var json = await _computerVision.callAPI(image, "Description");
            Description description = JsonConvert.DeserializeObject<Description>(JObject.Parse(json)["description"].ToString());

            Caption caption = description.Captions.FirstOrDefault();

            return caption?.Text ?? "";
        }
    }
}