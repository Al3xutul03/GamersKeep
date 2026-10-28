using AutoMapper;
using BusinessLogic.Json;
using BusinessLogic.Json.Model;
using BusinessLogic.Models.CartEntry;
using BusinessLogic.Models.Product;
using BusinessLogic.Models.User;
using Newtonsoft.Json;
using Repository.Entity;
using Repository.Enums.Types;

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

        CreateMap<ProductCreateModel, Product>()
            .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.Categories != null
                    ? src.Categories.Select(c => new ProductCategory { CategoryId = c }).ToList()
                    : new List<ProductCategory>()))
            .ForMember(dest => dest.Images,
                opt => opt.MapFrom(src => src.Images != null
                    ? src.Images.Select(i => new ProductImage { ImageUrl = i }).ToList()
                    : new List<ProductImage>()));

        CreateMap<Product, ProductReadModel>()
            .ForCtorParam(nameof(ProductReadModel.Categories),
                opt => opt.MapFrom(src => src.Categories != null
                    ? src.Categories.Select(pc => pc.CategoryId).ToList()
                    : new List<CategoryType>()))
            .ForCtorParam(nameof(ProductReadModel.Images),
                opt => opt.MapFrom(src => src.Images != null
                    ? src.Images.Select(pi => pi.ImageUrl).ToList()
                    : new List<string>()));

        CreateMap<Product, ProductListingModel>()
            .ForCtorParam(nameof(ProductListingModel.ShortDescription),
                opt => opt.MapFrom(src => src.Description.Length > 100
                    ? src.Description.Substring(0, 100) + "..."
                    : src.Description))
            .ForCtorParam(nameof(ProductListingModel.Categories),
                opt => opt.MapFrom(src => src.Categories != null
                    ? src.Categories.Select(pc => pc.CategoryId).ToList()
                    : new List<CategoryType>()))
            .ForCtorParam(nameof(ProductListingModel.FirstImageUrl),
                opt => opt.MapFrom(src => src.Images != null && src.Images.Any()
                    ? src.Images.First().ImageUrl
                    : string.Empty));

        CreateMap<ProductUpdateModel, Product>()
            .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.Categories != null
                    ? src.Categories.Select(c => new ProductCategory { ProductId = src.Id, CategoryId = c }).ToList()
                    : new List<ProductCategory>()))
            .ForMember(dest => dest.Images,
                opt => opt.MapFrom(src => src.Images != null
                    ? src.Images.Select(i => new ProductImage { ProductId = src.Id, ImageUrl = i }).ToList()
                    : new List<ProductImage>()));

        CreateMap<CartEntryCreateModel, CartEntry>();
        CreateMap<CartEntry, CartEntryReadModel>()
            .ForCtorParam(nameof(CartEntryReadModel.Product),
                opt => opt.MapFrom(src => src.Product));
        CreateMap<CartEntryUpdateModel, CartEntry>();
    }
}
