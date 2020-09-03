using System;
using System.Collections.Generic;
using System.Linq;
using AlbumApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AlbumApp.Data
{
    public class DBInitializer
    {
        private AlbumContext _context;

        public DBInitializer (AlbumContext context)
        {
            _context = context;
        }

        public async void SeedUser()
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Marko",
                LastName = "Damjanović",
                UserName = "damjanovic.m1@gmail.com",
                NormalizedUserName = "damjanovic.m1@gmail.com".ToUpper(),
                Email = "damjanovic.m1@gmail.com",
                NormalizedEmail = "damjanovic.m1@gmail.com".ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "Marko123!");
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user);
            }

            await _context.SaveChangesAsync();
        }
        public async void SeedAlbum()
        {   
            ApplicationUser user = _context.Users.First();
            if(!_context.Album.Any(u => u.UserId == user.Id))
            {
                List<Photo> photos = new List<Photo>{new Photo
                {   
                    Id = 1,
                    UserId = user.Id,
                    PhotoName = "1bf78af9-059d-40c5-9ba4-0a4e7207615d_dog.jpg",
                    Description = "a dog sitting on a bed",
                },
                new Photo
                {   
                    Id = 2,
                    UserId = user.Id,
                    PhotoName = "42a0474a-17ac-4a63-9554-063485ee69c1_Lisboa.jpg",
                    Description = "a bridge over a body of water",
                },
                new Photo
                {   
                    Id = 3,
                    UserId = user.Id,
                    PhotoName = "071106a1-4868-47df-bd6f-3188a6838ceb_Man-Silhouette.jpg",
                    Description = "a man sitting in a dark room",
                },
                new Photo
                {   
                    Id = 4,
                    UserId = user.Id,
                    PhotoName = "8531e40a-5ede-4208-b832-bfc8d90a2aef_NY.jpg",
                    Description = "a person flying a kite in the air",
                }};
                
                await _context.Album.AddRangeAsync(photos);
                await _context.SaveChangesAsync();
            }
        }
    }
}