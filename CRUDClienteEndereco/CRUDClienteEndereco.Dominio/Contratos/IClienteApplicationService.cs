using CRUDClienteEndereco.Dominio.Entidades;
using System.Collections.Generic;

namespace CRUDClienteEndereco.Dominio.Contratos
{
    public interface IClienteApplicationService
    {
        Cliente ObterCliente(long clientId);

        List<Cliente> ObterClientes(int pagina, int quantidadeRegistros, out int totalPaginas);

        void DeletarCliente(long clientId);
    }
}
