using AutoMapper;
using ECommerce_API.Dtos;
using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserInfoDto>();
            CreateMap<UserInfoForUpdateDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserInfoForUpdateDto>();
        }
    }
}
