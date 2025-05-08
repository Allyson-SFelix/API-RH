using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;

namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Request
{
    public class SetoresRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(255)]
        public string nome { get; set; }


        [Required(ErrorMessage = "A localização é obrigatório")]
        [MaxLength(500)]
        public string localizacao { get; set; }


        [JsonConstructor]
        public SetoresRequest(string nome, string localizacao)
        {
            this.nome = nome;
            this.localizacao = localizacao;
        }
    }
}
