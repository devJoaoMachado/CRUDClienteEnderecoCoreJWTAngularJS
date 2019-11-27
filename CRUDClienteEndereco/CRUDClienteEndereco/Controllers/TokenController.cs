using CRUDClienteEndereco.Configuration;
using CRUDClienteEndereco.Models;
using CRUDClienteEndereco.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDClienteEndereco.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        private readonly TokenSettings _tokenSettings;
        private readonly IUsuarioService _usuarioService;

        public TokenController(TokenSettings tokenSettings, IUsuarioService usuarioService)
        {
            _tokenSettings = tokenSettings;
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("request")]
        public IActionResult RequestToken(Usuario usuario)
        {
            if (_usuarioService.UsuarioValido(usuario, out var role))
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