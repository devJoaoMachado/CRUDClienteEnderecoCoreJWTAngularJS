using CRUDClienteEndereco.Models;

namespace CRUDClienteEndereco.Services.Interfaces
{
    public interface IUsuarioService
    {
        bool UsuarioValido(Usuario usuario, out string role);
    }
}
