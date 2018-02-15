using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace LifeManager.Rest.Utilities
{
    public interface IUserManagerWrapper
    {
        Task<IdentityResult> CreateAsync(User user, string password);

        Task<User> FindByNameAsync(string userName);

        Task<bool> CheckPasswordAsync(User user, string password);

        Task<User> GetUserAsync(ClaimsPrincipal principal);

        Task<string> GetUserIdAsync(User user);
    }
}
