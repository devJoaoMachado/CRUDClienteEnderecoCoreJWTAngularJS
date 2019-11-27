using CRUDClienteEndereco.Dominio.Entidades;
using FluentNHibernate.Mapping;

namespace CRUDClienteEndereco.Infra.Mapping
{
    public class EnderecoMap : ClassMap<Endereco>
    {
        public EnderecoMap()
        {
            Id(t => t.EnderecoId).Column("ENDERECO_ID").GeneratedBy.Sequence("ENDERECO_SQ");
            Map(t => t.Logradouro);
            Map(t => t.Numero);
            Map(t => t.Complemento);
            Map(t => t.Bairro);
            Map(t => t.UF);
            Map(t => t.Cep);
            References(t => t.Cliente);
            Table("ENDERECO");

        }
    }
}
