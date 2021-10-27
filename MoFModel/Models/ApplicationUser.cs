using System;
using Microsoft.AspNetCore.Identity;

namespace MoFModel.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string NickName { get; set; }
        public string Bio { get; set; }
        public int Age { get; set; }
    }
}
