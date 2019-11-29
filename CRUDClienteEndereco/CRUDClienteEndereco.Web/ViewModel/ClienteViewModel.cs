using System.ComponentModel.DataAnnotations;

namespace CRUDClienteEndereco.Web.ViewModel
{
    public class ClienteViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo tipo de documento é obrigatório")]
        public int TipoDocumento { get; set; }

        [Required(ErrorMessage = "O campo documento é obrigatório")]
        public long Documento { get; set; }

        [Required(ErrorMessage = "O campo logradouro é obrigatório")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O campo número é obrigatório, s/n deve ser informado 0.")]
        public int Numero { get; set; }
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo bairro é obrigatório.")]
        public string Bairro { get; set; }

        public string UF { get; set; }

        public string Cep { get; set; }
    }
}
