using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoFModel.Models;

namespace MoFApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("email")]
        public async Task<IActionResult> PostEmail(EmailLoginPostData data)
        {
            var result = await _signInManager.PasswordSignInAsync(data.Email, data.Password, false, false);
            if (result.Succeeded)
            {
                return Ok(new LoginResponse
                {
                    Code = ResultCode.Ok,
                    UserId = "",
                    Token = ""
                });
            }

            return Ok(new LoginResponse
            {
                Code = ResultCode.Fail,
                UserId = "",
                Token = ""
            });
        }

        [HttpPost("sso")]
        public async Task<IActionResult> PostSSO(SocialLoginPostData data)
        {
            var result = await _signInManager.ExternalLoginSignInAsync(data.Provider, data.Id, false);

            if (result.Succeeded)
            {
                return Ok(new LoginResponse
                {
                    Code = ResultCode.Ok,
                    Message = "login success",
                    UserId = "",
                    Token = ""
                });
            }

            var newUser = new ApplicationUser
            {
                UserName = data.Email,
                Email = data.Email,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(newUser);

            if (createResult.Succeeded)
            {
                var loginInfo = new UserLoginInfo(data.Provider, data.Id, data.Email);
                var addResult = await _userManager.AddLoginAsync(newUser, loginInfo);

                if (addResult.Succeeded)
                {
                    return Ok(new LoginResponse
                    {
                        Code = ResultCode.Ok,
                        Message = "join & login success",
                        UserId = "",
                        Token = ""
                    });
                }
            }

            return Ok(new LoginResponse
            {
                Code = ResultCode.Fail,
                Message = createResult.Errors.First().Description,
                UserId = "",
                Token = ""
            });
        }
    }
}