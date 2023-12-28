using AutoMapper;

using aluraflix_backend.Data.DAOs.IDAOs;
using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Services.IServices;
using aluraflix_backend.Models;

namespace aluraflix_backend.Services;
    public class VideoService : IVideoService
    {
        private readonly IVideoDAO _videoDAO;
        private readonly IMapper _mapper;

    public VideoService(IVideoDAO videoDAO, IMapper mapper)
    {
        _videoDAO = videoDAO;
        _mapper = mapper;
    }

    public IEnumerable<ReadVideoDTO> ExibirVideos(int page = 1)
    {
        var videos = _videoDAO.ExibirVideos(page);

        var videosDTO = _mapper.Map<List<ReadVideoDTO>>(videos);
        return videosDTO;
    }

    public IEnumerable<ReadVideoDTO> ExibirPorTitulo(string titulo)
    {
        var videos = _videoDAO.ExibirPorTitulo(titulo);
        
        var videosDTO = _mapper.Map<List<ReadVideoDTO>>(videos);
        return videosDTO;
    }

    public ReadVideoDTO ExibirVideoPorId(int id)
    {
        var video = _videoDAO.ExibirVideoPorId(id);

        var videoDTO = _mapper.Map<ReadVideoDTO>(video);
        return videoDTO;
    }

    public ReadVideoDTO CriarVideo(CreateVideoDTO videoDTO)
    {
        try
        {
            var video = _mapper.Map<CreateVideoDTO, Video>(videoDTO);

            if (video.CategoriaID == null)
            {
                video.CategoriaID = 1;
            }

            _videoDAO.Adicionar(video);
            _videoDAO.Salvar();
            
            return _mapper.Map<ReadVideoDTO>(video);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ocorreu um erro. Tente novamente. {ex.Message}");
        }
    }

    public ReadVideoDTO AtualizarVideo(int id, UpdateVideoDTO videoDTO)
    {
        try
        {
            var video = _videoDAO.ExibirVideoPorId(id);

            _mapper.Map(videoDTO, video);
            _videoDAO.Atualizar(video);
            _videoDAO.Salvar();

            return _mapper.Map<ReadVideoDTO>(video);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ocorreu um erro durante a atualização. Tente novamente. {ex.Message}");
        }
    }

    public UpdateVideoDTO BuscarVideoParaAtualizarParcial(int id)
    {
        var video = _videoDAO.ExibirVideoPorId(id);

        return _mapper.Map<UpdateVideoDTO>(video);
    }

    public void DeletarVideo(int id)
    {
        try
        {
            var video = _videoDAO.ExibirVideoPorId(id);

            _videoDAO.Deletar(video);
            _videoDAO.Salvar();

        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ocorreu um erro durante a atualização. Tente novamente. {ex.Message}");
        }

    }
}