using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 

using AlbumApp.Models;

namespace AlbumApp.Data
{
    public class AlbumContext : IdentityDbContext
    {
         public AlbumContext(DbContextOptions<AlbumContext> options): base(options)
         {}

    }
}