using AutoMapper;
using BusinessLogic.Json;
using BusinessLogic.Json.Model;
using BusinessLogic.Models.User;
using Newtonsoft.Json;
using Repository.Entity;

namespace BusinessLogic.Mapper;

/// <summary>
/// Business logic service for AutoMapper configuration.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        

        CreateMap<UserCreateModel, User>()
            .ForMember(dest => dest.Settings,
                opt => opt.MapFrom(src => JsonConvert.SerializeObject(new UserSettings(), AppJsonSettings.Default)));

        CreateMap<User, UserReadModel>()
            .ForCtorParam(nameof(UserReadModel.Settings),
                opt => opt.MapFrom(src => JsonConvert.DeserializeObject<UserSettings>(src.Settings, AppJsonSettings.Default)));

        CreateMap<UserUpdateModel, User>()
            .ForMember(dest => dest.Settings,
                opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Settings, AppJsonSettings.Default)));
    }
}
