using aluraflix_backend.Data.DTOs;

namespace aluraflix_backend.Services.IServices;
    public interface IVideoService
    {
        IEnumerable<ReadVideoDTO> ExibirVideos(int page);
        //IEnumerable<ReadVideoDTO> ExibirPrimeiraPaginaDeVideos();
        IEnumerable<ReadVideoDTO> ExibirPorTitulo(string titulo);
        ReadVideoDTO ExibirVideoPorId(int id);
        ReadVideoDTO CriarVideo(CreateVideoDTO videoDTO);
        ReadVideoDTO AtualizarVideo(int id, UpdateVideoDTO videoDTO);
        UpdateVideoDTO BuscarVideoParaAtualizarParcial(int id);
        void DeletarVideo(int id);
    }
