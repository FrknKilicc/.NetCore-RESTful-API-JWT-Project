using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProje.IServices;

namespace TestProje.Controllers.AccountController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public AccountController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(string username, string password)
        {
            if (username != "admin" && password != "admin")
                return Unauthorized("Invalid Credentials");
            else
                return new JsonResult(new { userName = username, token = _tokenService.CreateToken(username) });
        }
    }
}
