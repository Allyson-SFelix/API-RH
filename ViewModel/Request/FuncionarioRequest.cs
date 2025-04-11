using System.Text.Json.Serialization;

namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Request
{
    public class FuncionarioRequest
    {
        public string nome { get; set; }
        public DateTime dataEntrada { get; set; }

        public string cpf {  get; set; }

        public string setorNome {  get; set; }

        public float salario { get; set; }
        public DateTime dataNascimento { get; set; }

        [JsonConstructor]
        public FuncionarioRequest(string nome,string nomeSetor,string cpf,float salario,DateTime dataNascimento,DateTime dataEntrada) {
            this.nome = nome;
            this.setorNome = nomeSetor;
            this.cpf = cpf;
            this.salario = salario;
            this.dataEntrada = dataEntrada;
            this.dataNascimento = dataNascimento;
        }
       /* public FuncionarioRequest(string nome)
        {
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
        }
       */
    }
}

