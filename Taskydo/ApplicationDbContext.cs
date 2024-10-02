using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taskydo.Entities;

namespace Taskydo
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Entities.Task>().Property(t => t.title).HasMaxLength(250).IsRequired();
        }

        public DbSet<Entities.Task> tasks { get; set; }
        public DbSet<Step> steps { get; set; }
        public DbSet<AttachedFile> attachedFiles { get; set; }
    }
}
