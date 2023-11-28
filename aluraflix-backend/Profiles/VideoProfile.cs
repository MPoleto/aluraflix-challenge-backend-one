using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Models;
using AutoMapper;

namespace aluraflix_backend.Profiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, ReadVideoDTO>()
                .ForMember(dest => dest.CategoriaID, opt => opt.NullSubstitute(1));

            CreateMap<CreateVideoDTO, Video>()
                .ForMember(dest => dest.ID, opt => opt.Ignore());

            CreateMap<UpdateVideoDTO, Video>()
                .ForMember(dest => dest.ID, opt => opt.Ignore());

            CreateMap<Video, UpdateVideoDTO>();
        }
    }
}