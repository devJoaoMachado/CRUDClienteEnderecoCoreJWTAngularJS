using CRUDClienteEndereco.Web.Models;
using CRUDClienteEndereco.Web.Services.Interfaces;

namespace CRUDClienteEndereco.Web.Services
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        public bool UsuarioValido(Usuario usuario, out string role)
        {
            role = string.Empty;

            if (usuario.Login == "teste" && usuario.Senha == "crud")
            {
                role = "user";
                return true;
            }

            if (usuario.Login == "admin" && usuario.Senha == "crud")
            {
                role = "admin";
                return true;
            }

            return false;
        }
    }
}
