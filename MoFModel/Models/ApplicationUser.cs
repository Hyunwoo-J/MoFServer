using System;
using Microsoft.AspNetCore.Identity;

namespace MoFModel.Models
{
    /// <summary>
    /// 유저 정보
    /// </summary>
    public class ApplicationUser: IdentityUser
    {
        public string NickName { get; set; }
        public string Bio { get; set; }
        public int Age { get; set; }
    }
}
