using CRUDClienteEndereco.Models;
using CRUDClienteEndereco.Services.Interfaces;

namespace CRUDClienteEndereco.Services
{
    public class UsuarioService : IUsuarioService
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
