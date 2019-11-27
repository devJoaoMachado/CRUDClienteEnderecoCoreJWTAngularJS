using CRUDClienteEndereco.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDClienteEndereco.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    [Authorize()]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        [HttpPost]
        [Route("inserir")]
        public IActionResult Inserir()
        {
            return Ok();
        }

        [HttpPost]
        [Route("obter")]
        public IActionResult Obter()
        {
            return Ok(new {
                cliente = _clienteService.ObterCliente(1)
            });
        }

        [HttpPost]
        [Route("deletar")]
        [Authorize(Roles = "admin")]
        public IActionResult deletar()
        {
            return Ok();
        }


    }
}
