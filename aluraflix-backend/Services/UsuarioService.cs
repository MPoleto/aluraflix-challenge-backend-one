using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Models;
using aluraflix_backend.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace aluraflix_backend.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private ITokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task Cadastra(CreateUsuarioDTO usuarioDTO)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDTO);

            var resultado = await _userManager.CreateAsync(usuario, usuarioDTO.Password);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário");
            } 
        }

        public async Task<string> Login(LoginUsuarioDTO loginDTO)
        {
            var resultado = await _signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, false, false);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado");
            }

            var usuario = _signInManager
                .UserManager
                .Users.FirstOrDefault(user => user.NormalizedUserName == loginDTO.Username.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}