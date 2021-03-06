using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoFModel.Models;

namespace MoFApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : CommonController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        

        public LoginController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration) : base(configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// 이메일과 비밀번호로 로그인합니다.
        /// </summary>
        /// <param name="data"> 로그인 정보를 담은 객를 </param>
        /// <returns> 로그인 응답 객체 </returns>
        [HttpPost("email")]
        public async Task<IActionResult> PostEmail(EmailLoginPostData data)
        {
            var result = await _signInManager.PasswordSignInAsync(data.Email, data.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(data.Email);

                if (user != null)
                {
                    var token = GetApiToken(user);
                    if (!token.Contains("fail"))
                    {
                        return Ok(new LoginResponse
                        {
                            Code = ResultCode.Ok,
                            Message = "Login success",
                            UserId = user.Id,
                            Token = token
                        });
                    }
                    else
                    {
                        return Ok(new CommonResponse
                        {
                            Code = ResultCode.Fail,
                            Message = "Token not found"
                        });
                    }
                }
            }

            return Ok(new LoginResponse
            {
                Code = ResultCode.Fail,
                Message = "Login fail"
            });
        }

        /// <summary>
        /// SNS 계정으로 로그인합니다.
        /// </summary>
        /// <param name="data"> SNS 로그인 정보를 담은 객체 </param>
        /// <returns> 로그인 응답 객체 </returns>
        [HttpPost("sso")]
        public async Task<IActionResult> PostSSO(SocialLoginPostData data)
        {
            var result = await _signInManager.ExternalLoginSignInAsync(data.Provider, data.Id, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(data.Email);

                if (user != null)
                {
                    var token = GetApiToken(user);
                    if (!token.Contains("fail"))
                    {
                        return Ok(new LoginResponse
                        {
                            Code = ResultCode.Ok,
                            Message = "Login success",
                            UserId = user.Id,
                            Token = token
                        });
                    }
                    else
                    {
                        return Ok(new CommonResponse
                        {
                            Code = ResultCode.Fail,
                            Message = "Token not found"
                        });
                    }
                }
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
                    var token = GetApiToken(newUser);
                    if (!token.Contains("fail"))
                    {
                        return Ok(new LoginResponse
                        {
                            Code = ResultCode.Ok,
                            Message = "Join & Login success",
                            UserId = newUser.Id,
                            Token = token
                        });
                    }
                    else
                    {
                        return Ok(new CommonResponse
                        {
                            Code = ResultCode.Fail,
                            Message = "Token not found"
                        });
                    }
                }
            }

            return Ok(new LoginResponse
            {
                Code = ResultCode.Fail,
                Message = createResult.Errors.First().Description,
            });
        }
    }
}