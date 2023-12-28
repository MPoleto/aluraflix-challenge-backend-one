using aluraflix_backend.Data.DAOs.IDAOs;
using aluraflix_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace aluraflix_backend.Data.DAOs;
    public class CategoriaDAO : DAOBase<Categoria>, ICategoriaDAO
    {
        private AluraflixContext _context;

        public CategoriaDAO(AluraflixContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> ExibirCategorias(int page = 1)
        {
            int take = 5;
            int skip = take * (page - 1);
            
            return Exibir()
                .OrderBy(p => p.CategoriaID)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public Categoria ExibirCategoriaPorId(int id)
        {
            return Exibir().SingleOrDefault(p => p.CategoriaID == id);
        }

        public Categoria ExibirVideosPorCategoriaId(int id)
        {
            if (id == 1)
            {
                var videos = _context.Videos.Where(v => v.CategoriaID == null).ToList();

                foreach (var video in videos)
                {
                    video.CategoriaID = 1;
                }
                _context.SaveChanges();
            }
            return _context.Categorias.Include(c => c.Videos).AsNoTracking().SingleOrDefault(p => p.CategoriaID == id);
        }
}