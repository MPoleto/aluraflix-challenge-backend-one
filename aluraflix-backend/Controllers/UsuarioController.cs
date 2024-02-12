using Microsoft.AspNetCore.Mvc;

using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Services.IServices;

namespace aluraflix_backend.Controllers;

    /// <summary>
    /// Rota responsável pelas requisições referente aos usuários
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

        /// <summary>
        /// Cadastro de um novo usuário
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns>Exibe mensagem de confimação do cadastro</returns>
        [HttpPost("cadastro")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDTO usuarioDTO)
        {
            await _usuarioService.Cadastra(usuarioDTO);

            return Ok("Usuário cadastrado");
        }

        /// <summary>
        /// Autenticar o usuário
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns>Exibe token para autenticação</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginUsuarioDTO loginDTO)
        {
            var token = await _usuarioService.Login(loginDTO);

            return Ok(token);
        }
    }
