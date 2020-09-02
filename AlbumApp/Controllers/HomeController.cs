using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using AlbumApp.Models;
using AlbumApp.ViewModels;
using AlbumApp.Data;
using AlbumApp.Utility;
using System.Threading.Tasks;
using System.Linq;

namespace AlbumApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAlbumRepository _albumRepository;
        private readonly IImageService _imageHelper;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IAlbumRepository albumRepository, IImageService imageHelper, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _albumRepository = albumRepository;
            _imageHelper = imageHelper;
            _userManager = userManager;
        }

        public IActionResult Index()
        {   
            string userId = _userManager.GetUserId(User);
            AlbumViewModel model = new AlbumViewModel{ Photos = _albumRepository.GetAlbum().Where(p=> p.UserId == userId) };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadPhoto(AlbumViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(model.Photo != null)
                {
                    if(_imageHelper.ContentTypeValidation(model.Photo.ContentType))
                    {
                        uniqueFileName = await _imageHelper.UploadPhoto(model.Photo);
                        _imageHelper.CreateThumbnailImage(uniqueFileName);
                        
                        Photo newPhoto = new Photo {
                            UserId = _userManager.GetUserId(User), 
                            PhotoName = uniqueFileName, 
                            Description = await _imageHelper.GetDescription(uniqueFileName)};
 
                        _albumRepository.AddPhoto(newPhoto);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "File type not supported.");
                    }
                }                
            }
            
            return RedirectToAction("index", "home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
