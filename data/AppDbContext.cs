using blogg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace blogg.data
{
    public class AppDbContext:IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<AppUSer> AppUSers { get; set; }
        public DbSet<BlogModel>blogModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BlogModel>()
                .HasOne(b => b.appUSer)
                .WithMany(u => u.BlogModels)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
