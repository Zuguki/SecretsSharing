using AutoMapper;
using SecretsSharing.DAL.Models;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Mappers;

public class FileMapper : Profile
{
    public FileMapper()
    {
        CreateMap<FileModel, FileViewModel>();
        CreateMap<FileViewModel, FileModel>();
    }
}