using System.Collections.Generic;
using System.Linq;
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
           var result = _context.Album.Add(photo);
            _context.SaveChangesAsync();
        }

        public void DeletePhoto(Photo photo)
        {
            var result = _context.Album.Remove(photo);
            _context.SaveChangesAsync();
        }

        public IEnumerable<Photo> GetAlbum(string userId=null)
        {   
            if(userId != null)
            {
                return _context.Album.Where(p=> p.UserId == userId);
            }
            else
            {
                return _context.Album;
            }
        }
    }
}