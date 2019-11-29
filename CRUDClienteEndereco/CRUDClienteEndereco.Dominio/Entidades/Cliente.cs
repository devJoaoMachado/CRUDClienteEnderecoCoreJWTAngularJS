using System.Collections.Generic;

namespace CRUDClienteEndereco.Dominio.Entidades
{
    public class Cliente
    {
        public virtual int ClienteId { get; set; }
        public virtual string Nome { get; set; }
        public virtual long Documento { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual List<Endereco> Enderecos { get; set; }

        public Cliente()
        {
            Enderecos = new List<Endereco>();
        }
    }
}
