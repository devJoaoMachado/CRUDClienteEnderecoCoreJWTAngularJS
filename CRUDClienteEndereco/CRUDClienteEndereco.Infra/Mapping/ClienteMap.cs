using FluentNHibernate.Mapping;
using CRUDClienteEndereco.Dominio.Entidades;

namespace CRUDClienteEndereco.Infra.Mapping
{
    public class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Id(t => t.ClienteId).Column("CLIENTE_ID").GeneratedBy.Sequence("CLIENTE_SQ");
            Map(t => t.Nome).Column("NOME");
            Map(t => t.Documento).Column("DOCUMENTO");
            Map(t => t.TipodocumentoId).Column("TIPODOCUMENTO_ID");
            Table("CLIENTE");
        }
    }
}
