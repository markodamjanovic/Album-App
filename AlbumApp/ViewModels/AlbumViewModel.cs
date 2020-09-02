using System.Collections.Generic;
using AlbumApp.Models;
using Microsoft.AspNetCore.Http;

namespace AlbumApp.ViewModels
{
    public class AlbumViewModel
    {
        public IFormFile Photo {get; set;} 
        public IEnumerable<Photo> Photos {get; set;}
    }
}