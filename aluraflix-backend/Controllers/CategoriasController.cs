using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Services.IServices;
using Microsoft.AspNetCore.Authorization;

namespace aluraflix_backend.Controllers;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult TodasCategorias([FromQuery] int page = 1)
        {
            var categoriasDTO = _service.ExibirCategorias(page);

            if (!categoriasDTO.Any())
                return NotFound(new { msg = $"Não há categorias para exibir."});

            return Ok(categoriasDTO);
        }

        [HttpGet("{id}/videos")]
        public IActionResult VideosPorCategoriaId(int id)
        {
            var videosPorCategoriaDTO = _service.ExibirVideosPorCategoriaId(id);

            if (!videosPorCategoriaDTO.Videos.Any())
                return NotFound(new { msg = $"Não há vídeos na categoria com id {id}."});

            return Ok(videosPorCategoriaDTO);
        }

        [HttpGet("{id}")]
        public IActionResult CategoriaPorId(int id)
        {
            var categoriaDTO = _service.ExibirCategoriaPorId(id);

            if (categoriaDTO is null)
                return NotFound(new { msg = $"A categoria com id {id} não foi encontrada."});

            return Ok(categoriaDTO);
        }

        [HttpPost]
        public IActionResult CriarCategoria([FromBody] CreateCategoriaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriaDTO = _service.CriarCategoria(dto);
            
            return CreatedAtAction(nameof(CategoriaPorId), new {id = categoriaDTO.CategoriaID}, categoriaDTO);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCategoria(int id, [FromBody] UpdateCategoriaDTO dto)
        {
            var categoriaDTO = _service.AtualizarCategoria(id, dto);

            if (categoriaDTO is null)
                return NotFound(new { msg = $"A categoria com id {id} não foi encontrada."});

            return Ok(categoriaDTO);
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarCategoriaParcial(int id, JsonPatchDocument<UpdateCategoriaDTO> patch)
        {
            var categoriaParaAtualizar = _service.BuscarCategoriaParaAtualizarParcial(id);

            patch.ApplyTo(categoriaParaAtualizar, ModelState);

            if (!TryValidateModel(categoriaParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            
            var categoriaDTO = _service.AtualizarCategoria(id, categoriaParaAtualizar);

            return Ok(categoriaDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            _service.DeletarCategoria(id);

            return NoContent();
        }
    }