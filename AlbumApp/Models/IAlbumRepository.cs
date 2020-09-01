using System.Collections.Generic;
using AlbumApp.ViewModels;
using AlbumApp.Models;


namespace AlbumApp.Data
{
    public interface IAlbumRepository
    {
        IEnumerable<Photo> GetAlbum();
        Photo AddPhoto(Photo photo);
        Photo DeletePhoto(Photo photo);
    }
}