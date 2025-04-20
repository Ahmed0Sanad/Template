using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.Contract
{
    public interface IAuthService
    {
        public Task<string> GenerateTokenAsync(AppUser appUser, UserManager<AppUser> userManager);
    }
}
