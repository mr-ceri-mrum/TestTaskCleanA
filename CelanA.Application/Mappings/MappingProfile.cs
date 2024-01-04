using AutoMapper;
using CleanA.Domain.Dtos.Car;
using CleanA.Domain.DTOs.User;
using CleanA.Domain.Entitys.Car;
using CleanA.Domain.Entitys.User;

namespace CelanA.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Car, CarDto>()
            .ForMember(dest => dest.AddDateTime, opt => opt.MapFrom(src => src.AddedTime));
        CreateMap<ApplicationUser, UserDto>();
    }
}