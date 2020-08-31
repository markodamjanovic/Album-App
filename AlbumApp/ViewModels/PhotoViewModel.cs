using Microsoft.AspNetCore.Http;

namespace AlbumApp.ViewModels
{
    public class PhotoViewModel
    {
        public int UserId { get; set; }
        public IFormFile Photo {get; set;} 
        public string Description { get; set; }
    }
}