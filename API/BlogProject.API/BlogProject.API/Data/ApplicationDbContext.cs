﻿using BlogProject.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<BlogImage> BlogImages { get; set; }

    }
}
