using System.Text.Json.Serialization;

namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Request
{
    public class FuncionarioRequest
    {
        public string nome { get; set; }
        public int idade { get; set; }

        [JsonConstructor]
        public FuncionarioRequest(string nome,int idade) {
            this.nome = nome;
            this.idade = idade;
        }
        public FuncionarioRequest(string nome)
        {
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
        }

    }
}

