using CRUDClienteEndereco.Dominio.Entidades;
using FluentNHibernate.Mapping;

namespace CRUDClienteEndereco.Infra.Mapping
{
    public class TipoDocumentoMap : ClassMap<TipoDocumento>
    {
        public TipoDocumentoMap()
        {
            Id(t => t.TipoDocumentoId).Column("TIPODOCUMENTO_ID").GeneratedBy.Sequence("TIPODOCUMENTO_SQ");
            Map(t => t.Descricao);
            Table("TIPODOCUMENTO");
        }
    }
}
