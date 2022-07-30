
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using Forum._3.Models;
using Forum._3.Services;
using System;
using System.Collections.Generic;


namespace Forum._3.Data
{
    public class DBContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<Topic> Topics { set; get; }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            Role adminRole = new Role { Id = "1", Name = adminRoleName };
            Role userRole = new Role { Id = "2", Name = userRoleName };

            string adminEmail = "admin@gmail.com";
            string adminPassword = Crypto.HashPassword("123456");
            string userEmail = "user@gmail.com";
            string userPassword = Crypto.HashPassword("654321");

            User adminUser = new User { Id = Guid.NewGuid().ToString(), Username = "Alex", Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
            User simpleUser = new User { Id = Guid.NewGuid().ToString(), Username = "user", Email = userEmail, Password = userPassword, RoleId = userRole.Id };

            

            Topic topic1 = new Topic
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test topic 1",
                
            };

            Topic topic2 = new Topic
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test topic 2",
            };

            Post post1 = new Post
            {
                Id = Guid.NewGuid().ToString(),
                Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. ",
                DateCreation = DateTime.Now,
                UserId = adminUser.Id,
                TopicId = topic1.Id
            };

            Post post2 = new Post
            {
                Id = Guid.NewGuid().ToString(),
                Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. ",
                DateCreation = DateTime.Now,
                UserId = simpleUser.Id,
                TopicId = topic2.Id

            };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser, simpleUser });
            modelBuilder.Entity<Topic>().HasData(new Topic[] { topic1, topic2 });
            modelBuilder.Entity<Post>().HasData(new Post[] { post1, post2 });
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
