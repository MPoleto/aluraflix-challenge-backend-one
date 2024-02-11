using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace aluraflix_backend.Controllers;

    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDTO usuarioDTO)
        {
            await _usuarioService.Cadastra(usuarioDTO);

            return Ok("Usuário cadastrado");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDTO loginDTO)
        {
            var token = await _usuarioService.Login(loginDTO);

            return Ok(token);
        }
    }
