using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Services.IServices;

namespace aluraflix_backend.Controllers;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class VideosController : ControllerBase
    {
        private readonly IVideoService _service;

        public VideosController(IVideoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult TodosVideos([FromQuery] int page = 1)
        {
            var videosDTO = _service.ExibirVideos(page);

            if (!videosDTO.Any())
                return NotFound(new { msg = $"Não há vídeos para exibir."});

            return Ok(videosDTO);
        }

        [HttpGet("free")]
        [AllowAnonymous]
        public IActionResult PrimeiraPaginaDeVideos()
        {
            var videosDTO = _service.ExibirVideos(1);

            if (!videosDTO.Any())
                return NotFound(new { msg = $"Não há vídeos para exibir."});

            return Ok(videosDTO);
        }

        [HttpGet("titulo")]
        public IActionResult VideosPorTitulo([FromQuery] string titulo)
        {
            var videosDTO = _service.ExibirPorTitulo(titulo);

            if (!videosDTO.Any())
                return NotFound(new { msg = $"Nenhum vídeo encontrado."});

            return Ok(videosDTO);
        }

        [HttpGet("{id}")]
        public IActionResult VideoPorId(int id)
        {
            var videoDTO = _service.ExibirVideoPorId(id);

            if (videoDTO is null)
                return NotFound(new { msg = $"O vídeo com id {id} não foi encontrado."});

            return Ok(videoDTO);
        }

        [HttpPost]
        public IActionResult CriarVideo([FromBody] CreateVideoDTO videoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var videoCriado = _service.CriarVideo(videoDTO);
            
            return Created("Criado com sucesso", videoCriado);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarVideo(int id, [FromBody] UpdateVideoDTO dto)
        {
            var videoDTO = _service.AtualizarVideo(id, dto);

            if (videoDTO is null)
                return NotFound(new { msg = $"O vídeo com id {id} não foi encontrado."});

            return Ok(videoDTO);
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarVideoParcial(int id, JsonPatchDocument<UpdateVideoDTO> patch)
        {
            var videoParaAtualizar = _service.BuscarVideoParaAtualizarParcial(id);

            patch.ApplyTo(videoParaAtualizar, ModelState);

            if (!TryValidateModel(videoParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            
            var videoDTO = _service.AtualizarVideo(id, videoParaAtualizar);

            return Ok(videoDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarVideo(int id)
        {
            _service.DeletarVideo(id);

            return NoContent();
        }
    }