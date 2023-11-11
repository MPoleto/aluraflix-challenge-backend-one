using Microsoft.EntityFrameworkCore;
using aluraflix_backend.Models;

namespace aluraflix_backend.Data
{
    public class AluraflixContext : DbContext
    {
        public AluraflixContext(DbContextOptions<AluraflixContext> options) : base(options)
        {  
        }

        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Video>(table => {
                table.HasKey(p => p.ID);
                table.Property(p => p.ID)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
            });
        }
    }
}