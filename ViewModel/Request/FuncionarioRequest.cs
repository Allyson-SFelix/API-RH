using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Request
{
    public class FuncionarioRequest
    {

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(255)]
        public string nome { get; set; }

        [Required(ErrorMessage = "data de entrada é obrigatório")]
        public DateOnly dataEntrada { get; set; }


        [Required(ErrorMessage = "CPF é obrigatório")]
        public string cpf {  get; set; }


        [Required(ErrorMessage = "O nome do setor é obrigatório")]
        public string setorNome {  get; set; }


        [Required(ErrorMessage = "O salário é obrigatório")]        
        public float salario { get; set; }


        [Required(ErrorMessage = "A data de nascimento é obrigatório")]
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

