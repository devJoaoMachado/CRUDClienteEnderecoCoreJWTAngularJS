using CRUDClienteEndereco.Dominio.Contratos;
using CRUDClienteEndereco.Dominio.Entidades;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUDClienteEndereco.Web.Services
{
    public class ClienteApplicationService : IClienteApplicationService
    {
        private readonly ISession _session;

        public ClienteApplicationService(ISession session)
        {
            _session = session;
        }

        public void DeletarCliente(long clientId)
        {
            _session.CreateQuery("DELETE from cliente where cliente_id = :id")
                    .SetParameter("id", clientId)
                    .ExecuteUpdate();
        }

        public void InserirCliente(Cliente cliente)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(cliente);
                foreach (var endereco in cliente.Enderecos)
                {
                    endereco.ClienteId = cliente.ClienteId;
                    _session.Save(endereco);
                }

                transaction.Commit();
            }
        }

        public Cliente ObterCliente(long clientId)
        {
            var cliente = _session.Query<Cliente>().Where(c => c.ClienteId == clientId).FirstOrDefault();
            if (cliente != null)
                cliente.Enderecos = _session.Query<Endereco>().Where(e => e.ClienteId == clientId).ToList();

            return cliente;
        }

        public List<Cliente> ObterClientes(int pagina, int quantidadeRegistros, out int totalPaginas)
        {
            totalPaginas = (int)Math.Ceiling(_session.Query<Cliente>().Count() / Convert.ToDecimal(quantidadeRegistros));
            var clientes = _session.Query<Cliente>().OrderBy(c => c.ClienteId).Skip(quantidadeRegistros * (pagina - 1)).Take(quantidadeRegistros).ToList();

            foreach (var cliente in clientes)
            {
                cliente.Enderecos = _session.Query<Endereco>().Where(e => e.ClienteId == cliente.ClienteId).ToList();
            }

            return clientes;
        }
    }
}
