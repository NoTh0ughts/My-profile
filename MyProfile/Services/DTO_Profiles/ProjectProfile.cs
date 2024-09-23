using AutoMapper;
using Data.Models;
using Data.Models.DTO;

namespace MyProfile.Services.DTO_Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDTO>();
        CreateMap<Technology, TechnologyDTO>();
    }
}