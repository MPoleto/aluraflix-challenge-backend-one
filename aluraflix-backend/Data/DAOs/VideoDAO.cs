using aluraflix_backend.Data.DAOs.IDAOs;
using aluraflix_backend.Models;

namespace aluraflix_backend.Data.DAOs;
    public class VideoDAO : DAOBase<Video>, IVideoDAO
    {
        public VideoDAO(AluraflixContext context) : base(context)
        {
        }

        public IEnumerable<Video> ExibirVideos(int page = 1)
        {
            int take = 5;
            int skip = take * (page - 1);
            
            return Exibir()
                .OrderBy(p => p.ID)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public IEnumerable<Video> ExibirPorTitulo(string titulo)
        {
            return Exibir()
                .Where(video => video.Titulo.ToLowerInvariant().Contains(titulo.ToLowerInvariant()))
                .ToList();
        }

        public Video ExibirVideoPorId(int id)
        {
            return Exibir().SingleOrDefault(p => p.ID == id);
        }
    }