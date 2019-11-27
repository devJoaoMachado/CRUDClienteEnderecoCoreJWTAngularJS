using CRUDClienteEndereco.Dominio.Entidades;
using CRUDClienteEndereco.Services.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUDClienteEndereco.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ISession _session;

        public ClienteService(ISession session)
        {
            _session = session;
        }

        public Cliente ObterCliente(long clientId)
        {
            var clientes = _session.Query<Cliente>().Where(c => c.ClienteId == clientId);
            return clientes.FirstOrDefault();
        }

        public List<Cliente> ObterClientes(int pagina, int quantidadeRegistros, out int totalPaginas)
        {
            totalPaginas = (int)Math.Ceiling(_session.Query<Cliente>().Count() / Convert.ToDecimal(quantidadeRegistros));
            var clientes = _session.Query<Cliente>().OrderBy(c => c.ClienteId).Skip(quantidadeRegistros * (pagina - 1)).Take(quantidadeRegistros);

            return clientes.ToList();
        }
    }
}
