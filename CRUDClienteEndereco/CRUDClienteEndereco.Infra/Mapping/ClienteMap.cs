using CRUDClienteEndereco.Dominio.Entidades;
using FluentNHibernate.Mapping;

namespace CRUDClienteEndereco.Infra.Mapping
{
    public class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Id(t => t.ClienteId).Column("CLIENTE_ID").GeneratedBy.Sequence("CLIENTE_SQ");
            Map(t => t.Nome);
            Map(t => t.Documento);
            References(t => t.TipoDocumento);
            Table("CLIENTE");
        }
    }
}
