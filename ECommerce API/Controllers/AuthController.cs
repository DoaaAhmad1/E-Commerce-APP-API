using AutoMapper;
using ECommerce_API.Dtos;
using ECommerce_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AuthController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto model)
        {
          var Result = await _userRepository.RegisterUserAsync(model);
            if (Result.IsSuccess)
            {

                return Ok(Result);                
            }
            return BadRequest(Result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync( LoginDto model)
        {
          var Result = await _userRepository.LoginUserAsync(model);
           if (Result.IsSuccess)
            return Ok(Result);
          return BadRequest(Result);

        }
        [HttpGet("GetUserInfo")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
         
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).ToString();
            var User2 = UserId.Split(":");
            var User3 = User2[2].Trim();
            var UserFromRepo = _userRepository.GetUserInfo(User3);
            if (UserFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserInfoDto>(UserFromRepo));
        }
        [HttpPut("UpdateUserInfo")]
        [Authorize]
        public ActionResult UpdateUserInfo( UserInfoForUpdateDto userInfo)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).ToString();
            var User2 = UserId.Split(":");
            var User3 = User2[2].Trim();
            var UserFromRepo = _userRepository.GetUserInfo(User3);
            if (UserFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(userInfo, UserFromRepo);
            // AutoMapper Here Is Used For update 
            _userRepository.Save();
            return NoContent();
        }

    }
}
