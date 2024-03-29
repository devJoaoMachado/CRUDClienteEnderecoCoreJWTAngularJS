﻿using AutoMapper;
using CRUDClienteEndereco.Dominio.Contratos;
using CRUDClienteEndereco.Dominio.Entidades;
using CRUDClienteEndereco.Web.Extensions;
using CRUDClienteEndereco.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CRUDClienteEndereco.Web.Controllers
{
    [Controller]
    [Route("api/cliente")]
    [Authorize()]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteDomainService _clienteDomainService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteDomainService clienteDomainService, IMapper mapper)
        {
            _clienteDomainService = clienteDomainService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("inserir")]
        public IActionResult Inserir([FromBody] ClienteViewModel clienteViewModel)
        {
            return this.ExecuteAction(HttpContext, () =>
            {
                if (ModelState.IsValid)
                {
                    var cliente = _mapper.Map<Cliente>(clienteViewModel);
                    _clienteDomainService.InserirCliente(cliente);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState.Values);
                }
            });
        }

        [HttpGet]
        [Route("obter")]
        public IActionResult Obter([FromQuery(Name = "clienteId")]  long clienteId)
        {
            return this.ExecuteAction(HttpContext, () =>
            {
                var cliente = _clienteDomainService.ObterClientePorId(clienteId);
                if (cliente == null) return NotFound();
                var result = _mapper.Map<ClienteViewModel>(cliente);

                return Ok(result);

            });
        }

        [HttpGet]
        [Route("obtertodos")]
        public IActionResult ObterTodos([FromQuery(Name = "pagina")] int pagina = 1, [FromQuery(Name = "quantidadeRegistros")] int quantidadeRegistros = 10)
        {
            return this.ExecuteAction(HttpContext, () =>
            {
                var clientes = _clienteDomainService.ObterTodosClientes(pagina, quantidadeRegistros, out var totalPaginas);
                List<ClienteViewModel> result = new List<ClienteViewModel>();

                foreach (var cliente in clientes)
                {
                    result.Add(_mapper.Map<ClienteViewModel>(cliente));
                }

                HttpContext.Response.Headers.Add("X-TotalPages", totalPaginas.ToString());

                return Ok(result);
            });
        }


        [HttpDelete]
        [Route("deletar")]
        [Authorize(Roles = "admin")]
        public IActionResult deletar([FromQuery(Name = "clienteId")]  long clienteId)
        {
            return this.ExecuteAction(HttpContext, () =>
            {
                _clienteDomainService.DeletarCliente(clienteId);
                return NoContent();
            });

        }


    }
}
