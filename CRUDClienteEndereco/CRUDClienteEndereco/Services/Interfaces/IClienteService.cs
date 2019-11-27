using CRUDClienteEndereco.Dominio.Entidades;
using System.Collections.Generic;

namespace CRUDClienteEndereco.Services.Interfaces
{
    public interface IClienteService
    {
        Cliente ObterCliente(long clientId);
        List<Cliente> ObterClientes(int pagina, int quantidadeRegistros, out int totalPaginas);
    }
}
