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
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Video>()
                .HasKey(p => p.ID);

            builder.Entity<Video>()
                .Property(p => p.ID)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Entity<Video>()
                .HasOne(p => p.Categoria)
                .WithMany(categoria => categoria.Videos)
                .HasForeignKey(p => p.CategoriaID)
                .IsRequired(false);
            
            builder.Entity<Categoria>()
                .HasKey(p => p.CategoriaID);
            
            builder.Entity<Categoria>()
                .Property(p => p.CategoriaID)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Entity<Categoria>()
                .HasData( new { CategoriaID = 1, Titulo = "Livre", Cor = "#8c8c8c"});
            
        }
    }
}