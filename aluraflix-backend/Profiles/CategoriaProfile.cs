using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Models;
using AutoMapper;

namespace aluraflix_backend.Profiles;
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, ReadCategoriaDTO>();

            CreateMap<CreateCategoriaDTO, Categoria>()
                .ForMember(dest => dest.CategoriaID, opt => opt.Ignore());

            CreateMap<UpdateCategoriaDTO, Categoria>()
                .ForMember(dest => dest.CategoriaID, opt => opt.Ignore());

            CreateMap<Categoria, UpdateCategoriaDTO>();
        }
    }