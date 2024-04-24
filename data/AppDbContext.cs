using blogg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace blogg.data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUSer> AppUSers { get; set; }
        public DbSet<BlogModel> blogModels { get; set; }
        public DbSet<CommentModel> cmtModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BlogModel>()
                .HasOne(b => b.appUSer)
                .WithMany(u => u.BlogModels)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

        

            builder.Entity<CommentModel>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.commentModels)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or NoAction

            builder.Entity<CommentModel>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.commentModels)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
