using System.Text.Json.Serialization;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;

namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Request
{
    public class SetoresRequest
    {

        public string nome { get; set; }

        public int qtd_funcionarios { get; set; }

        public string localizacao { get; set; }


        [JsonConstructor]
        public SetoresRequest(string nome, int qtd_funcionarios, string localizacao)
        {
            this.nome = nome;
            this.qtd_funcionarios = qtd_funcionarios;
            this.localizacao = localizacao;
        }
    }
}
