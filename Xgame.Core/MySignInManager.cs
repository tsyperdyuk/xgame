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
        private readonly UserManager<AppUser> _userManager;

        public MySignInManager(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<AppUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor = null, ILogger<SignInManager<AppUser>> logger = null, IAuthenticationSchemeProvider schemes = null)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
            _userManager = userManager;
            _httpContextAccessor = contextAccessor;
        }
        
        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {           
            var userId = (await _userManager.FindByNameAsync(userName)).Id;
            var claims = new List<Claim>
            {
              new Claim(UserClaimTypes.UserName, userName),
              new Claim(UserClaimTypes.Id, userId),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            return SignInResult.Success;
        }

        public override Task SignOutAsync()
        {
            return _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);                
        }
    }
}
