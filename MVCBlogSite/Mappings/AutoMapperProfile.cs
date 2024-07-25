using AutoMapper;
using BLL.Dtos;
using MVCBlogSite.Models;

namespace MVCBlogSite.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, UserViewModel>().ReverseMap();
            CreateMap<PostDto, PostViewModel>().ReverseMap();
            CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
            CreateMap<ComplainDto, ComplainViewModel>().ReverseMap();
            CreateMap<PostLikeDto, PostLikeViewModel>().ReverseMap();
            CreateMap<PostCategoryDto, PostCategoryViewModel>().ReverseMap();
        }
    }
}
