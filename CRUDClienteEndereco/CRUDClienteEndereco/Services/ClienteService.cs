using CRUDClienteEndereco.Dominio.Entidades;
using CRUDClienteEndereco.Services.Interfaces;
using NHibernate;
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
    }
}
