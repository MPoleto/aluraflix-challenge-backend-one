using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Services.IServices;

namespace aluraflix_backend.Controllers;

    /// <summary>
    /// Rota responsável pelas requisições referente aos vídeos
    /// </summary>
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

        /// <summary>
        /// Exibe vídeos cadastrados
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Retorna 5 vídeos por página</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult TodosVideos([FromQuery] int page = 1)
        {
            var videosDTO = _service.ExibirVideos(page);

            if (!videosDTO.Any())
                return NotFound(new { msg = $"Não há vídeos para exibir."});

            return Ok(videosDTO);
        }

        /// <summary>
        /// Exibe vídeos sem ser necessário a autenticação
        /// </summary>
        /// <returns>Retorna os 5 primeiros vídeos cadastrados</returns>
        [HttpGet("free")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PrimeiraPaginaDeVideos()
        {
            var videosDTO = _service.ExibirVideos(1);

            if (!videosDTO.Any())
                return NotFound(new { msg = $"Não há vídeos para exibir."});

            return Ok(videosDTO);
        }

        /// <summary>
        /// Procura os vídeos que possuem a palavra buscada no título
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns>Se existir, exibe os vídeos com a palavra buscada no título</returns>
        [HttpGet("titulo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult VideosPorTitulo([FromQuery] string titulo)
        {
            var videosDTO = _service.ExibirPorTitulo(titulo);

            if (!videosDTO.Any())
                return NotFound(new { msg = $"Nenhum vídeo encontrado."});

            return Ok(videosDTO);
        }

        /// <summary>
        /// Exibe o vídeo com determinado ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Caso exista o ID, retorna as informações do vídeo</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult VideoPorId(int id)
        {
            var videoDTO = _service.ExibirVideoPorId(id);

            if (videoDTO is null)
                return NotFound(new { msg = $"O vídeo com id {id} não foi encontrado."});

            return Ok(videoDTO);
        }

        /// <summary>
        /// Cadastra um novo vídeo
        /// </summary>
        /// <param name="videoDTO"></param>
        /// <returns>Retorna as informações do vídeo cadastrado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarVideo([FromBody] CreateVideoDTO videoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var videoCriado = _service.CriarVideo(videoDTO);
            
            return Created("Criado com sucesso", videoCriado);
        }

        /// <summary>
        /// Atualiza as informações do vídeo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Exibe as informações recém atualizadas</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AtualizarVideo(int id, [FromBody] UpdateVideoDTO dto)
        {
            var videoDTO = _service.AtualizarVideo(id, dto);

            if (videoDTO is null)
                return NotFound(new { msg = $"O vídeo com id {id} não foi encontrado."});

            return Ok(videoDTO);
        }

        /// <summary>
        /// Atualização parcial do vídeo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns>Exibe as informações do vídeo atualizado</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Detela vídeo cadastrado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeletarVideo(int id)
        {
            _service.DeletarVideo(id);

            return NoContent();
        }
    }