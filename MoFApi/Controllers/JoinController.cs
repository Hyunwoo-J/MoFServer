using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoFModel.Models;

namespace MoFApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JoinController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public JoinController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("email")]
        public async Task<IActionResult> PostEmail(EmailJoinPostData data)
        {
            var user = new ApplicationUser
            {
                UserName = data.Email,
                Email = data.Email
            };

            var result = await _userManager.CreateAsync(user, data.Password);
            if (result.Succeeded)
            {
                return Ok(new JoinResponse
                {
                    Code = ResultCode.Ok,
                    UserId = "",
                    Token = ""
                });
            }

            return Ok(new JoinResponse
            {
                Code = ResultCode.Fail,
                Message = result.Errors.FirstOrDefault()?.Description,
                UserId = "",
                Token = ""
            });
        }
    }
}