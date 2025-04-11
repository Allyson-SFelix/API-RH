using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;


namespace API_ARMAZENA_FUNCIONARIOS.Model.Tables
{
    [Table("funcionarios")]
    public class ModelFuncionario
    {

        [Key]
        public int id { get; private set; }

        public string nome { get; set; }

        public DateTime dataEntrada { get; set; }

        public string cpf { get; set; }
        public int id_setor { get; set; }

        public float salario { get; set; }

        public DateTime data_nascimento { get; set; }
        public EnumStatus status { get; set; }
        public ModelFuncionario(string nome, DateTime dataEntrada, string cpf, int id_setor, float salario, DateTime data_nascimento, EnumStatus status)
        {
            this.nome = nome;
            this.dataEntrada = dataEntrada;
            this.cpf = cpf;
            this.id_setor = id_setor;
            this.salario = salario;
            this.data_nascimento = data_nascimento;
            this.status = status;
        }




    }
}
