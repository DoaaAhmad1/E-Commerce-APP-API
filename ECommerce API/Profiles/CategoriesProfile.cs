using AutoMapper;
using ECommerce_API.Dtos;
using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Profiles
{
    public class CategoriesProfile:Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryForUpdateAndCreateDto, Category>();
            CreateMap< Category, CategoryForUpdateAndCreateDto>();
        }
        
    }
}
