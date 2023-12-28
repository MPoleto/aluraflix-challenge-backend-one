using aluraflix_backend.Models;

namespace aluraflix_backend.Data.DAOs.IDAOs;
    public interface IVideoDAO : IDAOBase<Video>
    {
        IEnumerable<Video> ExibirVideos(int page = 1);
        IEnumerable<Video> ExibirPorTitulo(string titulo);
        Video ExibirVideoPorId(int id);
    }