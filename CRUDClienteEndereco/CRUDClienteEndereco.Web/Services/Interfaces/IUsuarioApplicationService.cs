using CRUDClienteEndereco.Web.Models;

namespace CRUDClienteEndereco.Web.Services.Interfaces
{
    public interface IUsuarioApplicationService
    {
        bool UsuarioValido(Usuario usuario, out string role);
    }
}
