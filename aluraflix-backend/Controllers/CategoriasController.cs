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
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaDAO _dao;
        private readonly IMapper _mapper;

        public CategoriasController(CategoriaDAO dao, IMapper mapper)
        {
            _dao = dao;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult TodasCategorias()
        {
            var categorias = _dao.BuscarCategorias();

            var resultado = _mapper.Map<List<ReadCategoriaDTO>>(categorias);

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult CategoriaPorId(int id)
        {
            var categoria = _dao.BuscarCategoriaPorId(id);

            if (categoria is null)
                return NotFound(new { msg = $"A categoria com id {id} não foi encontrada."});

            var categoriaDTO = _mapper.Map<ReadCategoriaDTO>(categoria);

            return Ok(categoriaDTO);
        }

        [HttpGet("{id}/videos")]
        public IActionResult VideosPorCategoriaId(int id)
        {
            var videos = _dao.BuscarVideosPorCategoriaId(id);

            if (!videos.Any())
                return NotFound(new { msg = $"Não há vídeos na categoria com id {id}."});

            var videosPorCategoria = _mapper.Map<List<ReadVideoDTO>>(videos);

            return Ok(videosPorCategoria);
        }

        [HttpPost]
        public IActionResult CriarCategoria([FromBody] CreateCategoriaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoria = _mapper.Map<CreateCategoriaDTO, Categoria>(dto);

            _dao.AdicionarCategoria(categoria);

            var categoriaDTO = _mapper.Map<ReadCategoriaDTO>(categoria);
            
            return CreatedAtAction(nameof(CategoriaPorId), new {id = categoria.CategoriaID}, categoriaDTO);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCategoria(int id, [FromBody] UpdateCategoriaDTO dto)
        {
            var categoria = _dao.BuscarCategoriaPorId(id);

            if (categoria is null)
                return NotFound(new { msg = $"A categoria com id {id} não foi encontrada."});

            _mapper.Map(dto, categoria);
            _dao.AtualizarCategoria(categoria);

            var categoriaDTO = _mapper.Map<ReadCategoriaDTO>(categoria);

            return Ok(categoriaDTO);
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarCategoriaParcial(int id, JsonPatchDocument<UpdateCategoriaDTO> patch)
        {
            var categoria = _dao.BuscarCategoriaPorId(id);

            if (categoria is null)
                return NotFound(new { msg = $"A categoria com id {id} não foi encontrada."});

            var categoriaParaAtualizar = _mapper.Map<UpdateCategoriaDTO>(categoria);

            patch.ApplyTo(categoriaParaAtualizar, ModelState);

            if (!TryValidateModel(categoriaParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(categoriaParaAtualizar, categoria);
            _dao.AtualizarCategoria(categoria);
            
            var categoriaDTO = _mapper.Map<ReadCategoriaDTO>(categoria);

            return Ok(categoriaDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            var categoria = _dao.BuscarCategoriaPorId(id);

            if (categoria is null)
                return NotFound(new { msg = $"A categoria com id {id} não foi encontrada."});

            _dao.DeletarCategoria(categoria);

            return NoContent();
        }
    }
}