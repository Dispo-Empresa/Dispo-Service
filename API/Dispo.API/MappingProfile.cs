using AutoMapper;
using Dispo.Infra.Core.Application.Models.Response;
using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseModel>();
        }

        public static MapperConfiguration CreateMappingProfile()
            => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
    }
}