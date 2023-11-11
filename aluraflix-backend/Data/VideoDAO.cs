using aluraflix_backend.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Videos
                    .AsNoTracking()
                    .SingleOrDefault(p => p.ID == id);
        }

        public void AdicionarVideo(Video video)
        {
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