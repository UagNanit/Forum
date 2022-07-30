
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using Forum._3.Models;
using Forum._3.Services;
using System;


namespace Forum._3.Data
{
    public class DBContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<Topic> Topicts { set; get; }
        public DbSet<Section> Sections { set; get; }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@gmail.com";
            string adminPassword = Crypto.HashPassword("123456");

            // добавляем роли
            Role adminRole = new Role { Id = "1", Name = adminRoleName };
            Role userRole = new Role { Id = "2", Name = userRoleName };
            User adminUser = new User { Id = Guid.NewGuid().ToString(), Username = "Alex", Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
