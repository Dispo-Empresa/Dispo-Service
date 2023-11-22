using AutoMapper;
using Dispo.Shared.Core.Domain.DTOs.Response;
using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDto>();
        }

        public static MapperConfiguration CreateMappingProfile()
            => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
    }
}