using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(x => x.Posts).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict); // OnDelete(DeleteBehavior.Restrict) -> User silindiğinde bağlı olduğu Post'lar silinmesin
            modelBuilder.Entity<User>().HasMany(x => x.Complains).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Post>().HasMany(x => x.Complains).WithOne(x => x.Post).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<User>().HasData(
                    new User { Id = 10, FirstName = "Ali", LastName = "Veli", BirthDate = DateTime.Now, UserName = "admin", Password = "123", IsAdmin = true, CreatedDate = DateTime.Now }

                );
        }
    }
}
