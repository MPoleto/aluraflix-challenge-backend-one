using aluraflix_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace aluraflix_backend.Data
{
    public class CategoriaDAO
    {
        private AluraflixContext _context;

        public CategoriaDAO(AluraflixContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> BuscarCategorias()
        {
            return _context.Categorias.ToList();
        }

        public Categoria BuscarCategoriaPorId(int id)
        {
            return _context.Categorias.SingleOrDefault(p => p.CategoriaID == id);
        }

        public IEnumerable<Video> BuscarVideosPorCategoriaId(int id)
        {
            if (id == 1)
            {
                return _context.Videos
                .Where(videos => videos.CategoriaID == null || videos.CategoriaID == id)
                .ToList();
            }

            return _context.Videos
                .Where(videos => videos.CategoriaID == id)
                .ToList();
        }

        public void AdicionarCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public void AtualizarCategoria(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
        }

        public void DeletarCategoria(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();  
        }
    }
}