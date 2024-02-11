using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aluraflix_backend.Data.DTOs;

namespace aluraflix_backend.Services.IServices
{
    public interface IUsuarioService
    {
        Task Cadastra(CreateUsuarioDTO usuarioDTO);

        Task<string> Login(LoginUsuarioDTO loginDTO);
    }
}