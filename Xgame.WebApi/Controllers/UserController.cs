using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xgame.Core;
using Xgame.Model;

namespace Xgame.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            var manager = new UserManager();
            var result = await manager.GetUsersAsync().ConfigureAwait(false);
            return Ok(result);
        }
    }
}