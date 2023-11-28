using aluraflix_backend.Models;

namespace aluraflix_backend.Data
{
    public class VideoDAO
    {
        private AluraflixContext _context;

        public VideoDAO(AluraflixContext context)
        {
            _context = context;
        }

        public IEnumerable<Video> BuscarVideos()
        {
            return _context.Videos.ToList();
        }

        public Video BuscarVideoPorId(int id)
        {
            return _context.Videos.SingleOrDefault(p => p.ID == id);
        }

        public IEnumerable<Video> BuscarVideosPorTitulo(string titulo)
        {
            return _context.Videos
                .Where(video => video.Titulo.Contains(titulo))
                .ToList();
        }

        public void AdicionarVideo(Video video)
        {
            video.CategoriaID ??= 1;
            
            _context.Videos.Add(video);
            _context.SaveChanges();
        }

        public void AtualizarVideo(Video video)
        {
            _context.Videos.Update(video);
            _context.SaveChanges();
        }

        public void DeletarVideo(Video video)
        {
            _context.Videos.Remove(video);
            _context.SaveChanges();  
        }
    }
}