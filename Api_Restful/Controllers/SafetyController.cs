using Api_Restful.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Api_Restful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SafetyController : ControllerBase
    {
        private IConfiguration _config;
        public SafetyController(IConfiguration Configuration)
        {
            _config = Configuration;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] TokenLogin login)
        {
            bool resultado = ValidarUsuario(login);
            if (resultado)
            {
                var tokenString = GerarToken();
                return Ok(new TokenRetorno { Token = tokenString, DataTokenGerado = DateTime.Now });
            }
            else
            {
                return Unauthorized();
            }
        }
        private bool ValidarUsuario(TokenLogin login)
        {
            if (login.Usuario == "RaulHenriqueFurtado" && login.Senha == "SenhaAlterada")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string GerarToken()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: expiry,
                signingCredentials: credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
