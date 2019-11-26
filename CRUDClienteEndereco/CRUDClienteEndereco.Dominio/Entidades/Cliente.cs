namespace CRUDClienteEndereco.Dominio.Entidades
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public long Documento { get; set; }
        public int TipodocumentoId { get; set; }

    }
}
