using ECommerce_API.Dtos;
using ECommerce_API.Interfaces;
using ECommerce_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> _userManger;
        private readonly AppDBContext _appDBContext;
        public UserRepository(UserManager<ApplicationUser> userManger, AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
            _userManger = userManger;

        }
        public async Task<UserMangerResponse> LoginUserAsync(LoginDto model)
        {
            var User = await _userManger.FindByEmailAsync(model.Email);
            if(User==null)
            {
                return new UserMangerResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false
                };
            }
            var result = await _userManger.CheckPasswordAsync(User, model.Password);
            if (!result)
            {
                return new UserMangerResponse
                {
                    Message = "Invalid Password",
                    IsSuccess = false
                };

            }

            //           var claims = new[]
            //{
            //               new Claim("Email",model.Email),
            //               new Claim(ClaimTypes.NameIdentifier,User.Id)
            //           };

            ////////////////////////////////////////////////////////////////
            var role = await _userManger.GetRolesAsync(User);
            var claims = new List<Claim>();

            claims.Add(new Claim("Email", model.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id));
            claims.Add(new Claim("ID", User.Id));
            foreach (var r in role)
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
                claims.Add(new Claim("Role", r));
            }


            /////////////////////////////////////////////////////////////////
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the key that we will use in the encryption"));

            var token = new JwtSecurityToken(issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenasString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserMangerResponse
            {
                Message = tokenasString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<UserMangerResponse> RegisterUserAsync(RegisterDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.Password != model.ConfirmPassword)
                return new UserMangerResponse()
                {
                    Message = "Confirm Password doesn't match the password",
                    IsSuccess = false
                };

            var ApplicationUser = new ApplicationUser
            {
                
                Email = model.Email,
                UserName = model.Email,
                FName = model.FName,
                LName = model.LName,
                Gender = model.Gender,
                BrithDate = model.BrithDate,
                Photo = model.Photo
            };

            var Result = await _userManger.CreateAsync(ApplicationUser, model.Password);
              
            if (Result.Succeeded)
            {
                //await _userManger.AddToRoleAsync(ApplicationUser, "admin");
                await _userManger.AddToRoleAsync(ApplicationUser, "user");

                return new UserMangerResponse
                {
                    Message = "User Was Created Successfully",
                    IsSuccess = true
                };
            }
            return new UserMangerResponse
            {
                Message = "User Was not Created",
                IsSuccess = false,
                Errors = Result.Errors.Select(e => e.Description)
            };



        }

        public ApplicationUser GetUserInfo(string UserId)
        {
            if (UserId == null)
            {
                throw new ArgumentNullException(nameof(UserId));
            }
            return _appDBContext.ApplicationUsers.SingleOrDefault(u => u.Id == UserId);
        }
        public bool Save()
        {
            return (_appDBContext.SaveChanges() >= 0);
        }



    }
}
