using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using PercobaanApi1.Helpers;
using PercobaanApi1.Models;

namespace PercobaanApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly SqlDBHelper _sqlDBHelper;

        public AdminController(IConfiguration config, SqlDBHelper sqlDBHelper)
        {
            _config = config;
            _sqlDBHelper = sqlDBHelper;
        }

        [AllowAnonymous]
        [HttpPost("LOGIN")]
        public async Task<IActionResult> Login([FromBody] Admin login)
        {
            IActionResult response = Unauthorized();

            var admin = await _sqlDBHelper.GetAdminByUsernameAsync(login.Username);

            if (admin != null && admin.Password == login.Password)
            {
                var tokenString = GenerateJSONWebToken(admin);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(Admin admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

