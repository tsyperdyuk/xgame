using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xgame.Db.Entities;

namespace Xgame.Core
{
    public class MySignInManager : SignInManager<AppUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public MySignInManager(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<AppUser> claimsFactory, 
            IOptions<IdentityOptions> optionsAccessor = null, ILogger<SignInManager<AppUser>> logger = null, IAuthenticationSchemeProvider schemes = null)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
            _userManager = userManager;
            _httpContextAccessor = contextAccessor;
        }
        
        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {           
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(UserClaimTypes.UserName, userName),
                new Claim(UserClaimTypes.UserId, user.Id)
            };
            roles.ToList().ForEach(r => claims.Add(new Claim(UserClaimTypes.Roles, r)));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, UserClaimTypes.UserName, UserClaimTypes.Roles);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return SignInResult.Success;
        }

        public override Task SignOutAsync()
        {
            return _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);                
        }
    }
}