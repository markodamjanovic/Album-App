using Microsoft.EntityFrameworkCore;

using AlbumApp.Models;

namespace AlbumApp.Data
{
    public class AlbumContext : DbContext
    {
         public AlbumContext(DbContextOptions<AlbumContext> options): base(options)
         {}

         public DbSet<User> Users {get; set;}
    }
}