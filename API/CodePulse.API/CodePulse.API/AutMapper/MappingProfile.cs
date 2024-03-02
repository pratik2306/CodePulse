using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace CodePulse.API.AutMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<CreateCategoryRequestDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<UpdateCategoryRequestDto, Category>();

            CreateMap<CreateBlogPostRequestDTO, BlogPost>().ForMember(dest=> dest.Categories, opt=> opt.Ignore());
            CreateMap<BlogPost, BlogPostDTO>().ForMember(d=> d.Categories, opts=> opts.MapFrom(src=> src.Categories));
            CreateMap<UpdateBlogPostRequestDTO, BlogPost>().ForMember(dest => dest.Categories, opt => opt.Ignore());

            CreateMap<BlogImage, BlogImageDTO>();

            CreateMap<RegisterUserRequestDTO, IdentityUser>()
                .ForMember(d=> d.Email, opt=> opt.MapFrom(opt=> opt.Email.Trim()))
                .ForMember(d => d.UserName, opt => opt.MapFrom(opt => opt.Email.Trim()));
        }
    }
}
