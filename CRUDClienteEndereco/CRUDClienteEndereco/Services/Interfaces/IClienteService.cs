using CRUDClienteEndereco.Dominio.Entidades;

namespace CRUDClienteEndereco.Services.Interfaces
{
    public interface IClienteService
    {
        Cliente ObterCliente(long clientId);
    }
}
