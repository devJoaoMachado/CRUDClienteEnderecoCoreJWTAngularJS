namespace CRUDClienteEndereco.Dominio.Entidades
{
    public class Endereco
    {
        public virtual long EnderecoId { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual int Numero { get; set; }
        public virtual  string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string UF { get; set; }
        public virtual string Cep { get; set; }
        public virtual long ClienteId { get; set; }
    }
}
