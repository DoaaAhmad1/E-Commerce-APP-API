using ECommerce_API.Dtos;
using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Interfaces
{
    public interface IUserRepository
    {
        Task<UserMangerResponse> RegisterUserAsync(RegisterDto model);
        Task<UserMangerResponse> LoginUserAsync(LoginDto model);
        ApplicationUser GetUserInfo(string UserId);
        bool Save();
    }
}
