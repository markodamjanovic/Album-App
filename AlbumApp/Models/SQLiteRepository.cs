using System.Collections.Generic;
using AlbumApp.ViewModels;
using AlbumApp.Models;

namespace AlbumApp.Data
{
    public class SQLiteRepository : IAlbumRepository
    {   
        private readonly AlbumContext _context;
        public SQLiteRepository(AlbumContext context)
        {
            _context = context;    
        }
        public Photo AddPhoto(Photo photo)
        {
            // here will be check if num of photos are > 5 
            // and AlbumViewModel will be as parameter
           var result = _context.Album.Add(photo);
           return photo;
        }

        public Photo DeletePhoto(Photo photo)
        {
            var result = _context.Album.Remove(photo);
            return photo;
        }

        public IEnumerable<Photo> GetAlbum()
        {
            return _context.Album;
        }
    }
}