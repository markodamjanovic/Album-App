using System.Collections.Generic;
using AlbumApp.ViewModels;
using AlbumApp.Models;


namespace AlbumApp.Data
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> GetAlbum();
        Album AddPhoto(Album photo);
        Album DeletePhoto(Album photo);
    }

}