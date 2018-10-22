using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xgame.Db.Entities;
using Xgame.Model;

namespace Xgame.Core
{
    public class MySignInManager : SignInManager<AppUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor; 

        public MySignInManager(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<AppUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor = null, ILogger<SignInManager<AppUser>> logger = null, IAuthenticationSchemeProvider schemes = null)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
            _httpContextAccessor = contextAccessor;
        }
        
        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var claims = new[] { new Claim("", "") };
            var identity = new ClaimsIdentity(claims, "local", "name", "role");
            var principal = new ClaimsPrincipal(identity);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return SignInResult.Success;
        }


        public override Task SignOutAsync()
        {
            return _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);                
        }
    }
}
