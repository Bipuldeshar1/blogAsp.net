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
                .HasOne(b => b.appUSer)//blog model has one user $ b.appUSer from blogmodel
                .WithMany(u => u.BlogModels)//App usermodel has many reln with blog  from appuser 
                .HasForeignKey(b => b.AuthorId)//fk in blogmodel
                .OnDelete(DeleteBehavior.Cascade);//if user is deleted all associated blog deleted
        }
    }
}
