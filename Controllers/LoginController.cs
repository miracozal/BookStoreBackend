using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IOptions<ConfigurationSettings> _configurationSettings;
        private readonly JwtSettings _jwtSettings;
        public LoginController(IOptions<JwtSettings> jwtSettings, IOptions<ConfigurationSettings> config)
        {
            _jwtSettings = jwtSettings.Value;
            _configurationSettings = config;
        }

        [HttpGet("Login")]
        public ActionResult Login(string email, string sifre)
        {
            var user = LoginService.Login(email, sifre, _configurationSettings.Value.ConnectionString);
            if (user != null)
            {
                var token = LoginService.GenerateJwtToken(user, _jwtSettings);
                return Ok(new { token, user.ID });
            }
            else
            {
                return Ok("Email ya da þifre hatalý");
            }
        }
    }
}
