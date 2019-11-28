using CRUDClienteEndereco.Services.Interfaces;
using CRUDClienteEndereco.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
        public IActionResult Inserir(ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else
            {
                //retornar erros
                return BadRequest();
            }

            
        }

        [HttpPost]
        [Route("obterTodos")]
        public IActionResult ObterTodos(int pagina =1, int quantidadeRegistros =10)
        {
            var result = _clienteService.ObterClientes(pagina, quantidadeRegistros, out var totalPaginas);
           
            HttpContext.Response.Headers.Add("X-Pages-TotalPages", totalPaginas.ToString());

            return Ok(result);
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
