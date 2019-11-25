using CRUDClienteEndereco.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDClienteEndereco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken(Usuario usuario)
        {
            if (usuario.Login == "teste" && usuario.Senha == "crud")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Login)
                };

                var securityKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var credentials =
                    new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                var securityToken =
                    new JwtSecurityToken(
                        issuer: "ApiSegura",
                        audience: "ApiSegura",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: credentials
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(securityToken)
                });
            }
            else
            {
                return BadRequest("Credenciais inválidas.");
            }
        }


    }
}