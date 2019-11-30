using CRUDClienteEndereco.Dominio.Contratos;
using CRUDClienteEndereco.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace CRUDClienteEndereco.Dominio.Servicos
{
    public class ClienteDomainService : IClienteDomainService
    {
        private readonly IClienteApplicationService _clienteService;

        public ClienteDomainService(IClienteApplicationService clienteService)
        {
            _clienteService = clienteService;
        }

        public void DeletarCliente(long clienteId)
        {
            if (clienteId > 0)
                _clienteService.DeletarCliente(clienteId);
        }

        public void InserirCliente(Cliente cliente)
        {
            _clienteService.InserirCliente(cliente);
        }

        public Cliente ObterClientePorId(long clienteId)
        {
            if (clienteId == 0)
                throw new ArgumentException("Informe um id de cliente válido.");

            return _clienteService.ObterCliente(clienteId);
        }

        public List<Cliente> ObterTodosClientes(int pagina, int quantidadeRegistros, out int totalPaginas)
        {
            return _clienteService.ObterClientes(pagina, quantidadeRegistros, out totalPaginas);
        }
    }
}
