using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 

using AlbumApp.Models;
using System;
namespace AlbumApp.Data
{
    public class AlbumContext : IdentityDbContext<ApplicationUser>
    {   
         public AlbumContext(DbContextOptions<AlbumContext> options): base(options)
         {
         }

         public DbSet<Photo> Album {get; set;}
    }
}