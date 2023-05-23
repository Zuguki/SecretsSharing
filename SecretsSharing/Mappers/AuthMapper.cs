using AutoMapper;
using SecretsSharing.DAL.Models;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Mappers;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<UserModel, RegisterViewModel>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        CreateMap<RegisterViewModel, UserModel>();
    }
}