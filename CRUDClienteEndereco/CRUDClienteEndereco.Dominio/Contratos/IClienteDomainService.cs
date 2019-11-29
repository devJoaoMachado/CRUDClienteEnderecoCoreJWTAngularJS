using CRUDClienteEndereco.Dominio.Entidades;
using System.Collections.Generic;

namespace CRUDClienteEndereco.Dominio.Contratos
{
    public interface IClienteDomainService
    {
        Cliente ObterClientePorId(long clienteId);

        List<Cliente> ObterTodosClientes(int pagina, int quantidadeRegistros, out int totalPaginas);

        void DeletarCliente(long clienteId);
    }
}
