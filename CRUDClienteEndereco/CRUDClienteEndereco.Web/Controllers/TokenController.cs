using CRUDClienteEndereco.Web.Configuration;
using CRUDClienteEndereco.Web.Models;
using CRUDClienteEndereco.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDClienteEndereco.Web.Controllers
{
    [Controller]
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        private readonly TokenSettings _tokenSettings;
        private readonly IUsuarioApplicationService _usuarioApplicationService;

        public TokenController(TokenSettings tokenSettings, IUsuarioApplicationService usuarioApplicationService)
        {
            _tokenSettings = tokenSettings;
            _usuarioApplicationService = usuarioApplicationService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("request")]
        public IActionResult RequestToken([FromBody] Usuario usuario)
        {
            if (_usuarioApplicationService.UsuarioValido(usuario, out var role))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Login),
                    new Claim(ClaimTypes.Role, role)
                };

                var securityKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey));
                var credentials =
                    new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var securityToken =
                    new JwtSecurityToken(
                        issuer: _tokenSettings.Issuer,
                        audience: _tokenSettings.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
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