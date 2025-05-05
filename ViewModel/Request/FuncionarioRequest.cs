using System.Text.Json.Serialization;

namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Request
{
    public class FuncionarioRequest
    {
        public string nome { get; set; }
        public DateOnly dataEntrada { get; set; }

        public string cpf {  get; set; }

        public string setorNome {  get; set; }

        public float salario { get; set; }
        public DateOnly dataNascimento { get; set; }


        [JsonConstructor]
        public FuncionarioRequest(string nome,string setorNome, string cpf,float salario, DateOnly dataNascimento, DateOnly dataEntrada) {
            this.nome = nome;
            this.setorNome = setorNome;
            this.cpf = cpf;
            this.salario = salario;
            this.dataNascimento = dataNascimento;
            this.dataEntrada = dataEntrada;
        }

       
    }
}

