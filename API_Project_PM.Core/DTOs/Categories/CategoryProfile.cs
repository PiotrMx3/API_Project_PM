using API_Project_PM.Core.Models;
using AutoMapper;

namespace API_Project_PM.Core.DTOs.Categories
{
    public  class CategoryProfile : Profile 
    {   
       
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        }

    }
}
