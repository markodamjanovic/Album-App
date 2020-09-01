using System.Collections.Generic;
using AlbumApp.Models;

namespace AlbumApp.Data
{
    public interface IAlbumRepository
    {
        IEnumerable<Photo> GetAlbum();
        void AddPhoto(Photo photo);
        void DeletePhoto(Photo photo);
    }
}