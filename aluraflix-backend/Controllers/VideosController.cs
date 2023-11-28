using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using aluraflix_backend.Data;
using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace aluraflix_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly VideoDAO _dao;
        private readonly IMapper _mapper;

        public VideosController(VideoDAO dao, IMapper mapper)
        {
            _dao = dao;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult TodosVideos()
        {
            var videos = _dao.BuscarVideos();

            var resultado = _mapper.Map<List<ReadVideoDTO>>(videos);

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult VideoPorId(int id)
        {
            var video = _dao.BuscarVideoPorId(id);

            if (video is null)
                return NotFound(new { msg = $"O vídeo com id {id} não foi encontrado."});

            var videoDTO = _mapper.Map<ReadVideoDTO>(video);

            return Ok(videoDTO);
        }

        [HttpGet("titulo")]
        public IActionResult VideosPorTitulo([FromQuery] string titulo)
        {
            var videos = _dao.BuscarVideosPorTitulo(titulo);

            if (!videos.Any())
                return NotFound(new { msg = $"Nenhum vídeo encontrado."});

            var videosDTO = _mapper.Map<List<ReadVideoDTO>>(videos);

            return Ok(videosDTO);
        }

        [HttpPost]
        public IActionResult CriarVideo([FromBody] CreateVideoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var video = _mapper.Map<CreateVideoDTO, Video>(dto);

            _dao.AdicionarVideo(video);

            var videoDTO = _mapper.Map<ReadVideoDTO>(video);
            
            return CreatedAtAction(nameof(VideoPorId), new {id = video.ID}, videoDTO);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarVideo(int id, [FromBody] UpdateVideoDTO dto)
        {
            var video = _dao.BuscarVideoPorId(id);

            if (video is null)
                return NotFound(new { msg = $"O vídeo com id {id} não foi encontrado."});

            _mapper.Map(dto, video);
            _dao.AtualizarVideo(video);

            var videoDTO = _mapper.Map<ReadVideoDTO>(video);

            return Ok(videoDTO);
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarVideoParcial(int id, JsonPatchDocument<UpdateVideoDTO> patch)
        {
            var video = _dao.BuscarVideoPorId(id);

            if (video is null)
                return NotFound(new { msg = $"O vídeo com id {id} não foi encontrado."});

            var videoParaAtualizar = _mapper.Map<UpdateVideoDTO>(video);

            patch.ApplyTo(videoParaAtualizar, ModelState);

            if (!TryValidateModel(videoParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(videoParaAtualizar, video);
            _dao.AtualizarVideo(video);
            
            var videoDTO = _mapper.Map<ReadVideoDTO>(video);

            return Ok(videoDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarVideo(int id)
        {
            var video = _dao.BuscarVideoPorId(id);

            if (video is null)
                return NotFound(new { msg = $"O vídeo com id {id} não foi encontrado."});

            _dao.DeletarVideo(video);

            return NoContent();
        }
    }
}