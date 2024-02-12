using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Services.IServices;

namespace aluraflix_backend.Controllers;

    /// <summary>
    /// Rota responsável pelas requisições referente às categorias dos vídeos
    /// </summary>
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

        /// <summary>
        /// Retorna as categorias criadas. 
        /// </summary>
        /// <param name="page"></param>
        /// <returns>5 categorias por página</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult TodasCategorias([FromQuery] int page = 1)
        {
            var categoriasDTO = _service.ExibirCategorias(page);

            if (!categoriasDTO.Any())
                return NotFound(new { msg = $"Não há categorias para exibir."});

            return Ok(categoriasDTO);
        }

        /// <summary>
        /// Exibe todos os vídeos adicionados a categoria com determinado ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de vídeos, quando há vídeos na categoria</returns>
        [HttpGet("{id}/videos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult VideosPorCategoriaId(int id)
        {
            var videosPorCategoriaDTO = _service.ExibirVideosPorCategoriaId(id);

            if (!videosPorCategoriaDTO.Videos.Any())
                return NotFound(new { msg = $"Não há vídeos na categoria com id {id}."});

            return Ok(videosPorCategoriaDTO);
        }

        /// <summary>
        /// Exibe informações de uma categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns>As informações da categoria do ID buscado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CategoriaPorId(int id)
        {
            var categoriaDTO = _service.ExibirCategoriaPorId(id);

            if (categoriaDTO is null)
                return NotFound(new { msg = $"A categoria com id {id} não foi encontrada."});

            return Ok(categoriaDTO);
        }

        /// <summary>
        /// Cria uma categoria
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Exibe a categoria criada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarCategoria([FromBody] CreateCategoriaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriaDTO = _service.CriarCategoria(dto);
            
            return CreatedAtAction(nameof(CategoriaPorId), new {id = categoriaDTO.CategoriaID}, categoriaDTO);
        }

        /// <summary>
        /// Atualizar todas as informações da categoria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Exibe as informações recém atualizadas</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AtualizarCategoria(int id, [FromBody] UpdateCategoriaDTO dto)
        {
            var categoriaDTO = _service.AtualizarCategoria(id, dto);

            if (categoriaDTO is null)
                return NotFound(new { msg = $"A categoria com id {id} não foi encontrada."});

            return Ok(categoriaDTO);
        }

        /// <summary>
        /// Atualização parcial de uma categoria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns>Exibe as informações da categoria</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Deleta uma categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeletarCategoria(int id)
        {
            _service.DeletarCategoria(id);

            return NoContent();
        }
    }