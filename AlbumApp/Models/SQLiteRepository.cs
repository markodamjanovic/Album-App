using System.Collections.Generic;
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
        public void AddPhoto(Photo photo)
        {
            // here will be check if num of photos are > 5 
            // and AlbumViewModel will be as parameter
           var result = _context.Album.Add(photo);
            _context.SaveChangesAsync();
        }

        public void DeletePhoto(Photo photo)
        {
            var result = _context.Album.Remove(photo);
            _context.SaveChangesAsync();
        }

        public IEnumerable<Photo> GetAlbum()
        {
            return _context.Album;
        }
    }
}