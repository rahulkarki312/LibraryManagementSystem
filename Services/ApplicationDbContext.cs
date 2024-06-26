﻿using LibraryManagement.Models;
using LibraryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options): base(options) {
        }

        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BurrowRecord> BurrowRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";

            var seller = new IdentityRole("seller");
            seller.NormalizedName = "seller";

            builder.Entity<IdentityRole>().HasData(admin, client, seller);


        }
        
    }
}
