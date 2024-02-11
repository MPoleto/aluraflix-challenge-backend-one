using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Models;
using AutoMapper;

namespace aluraflix_backend.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDTO, Usuario>();
        }
    }
}