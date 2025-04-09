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

        public required string cpf { get; set; }
        public int id_setor { get; set; }

        public float salario { get; set; }

        public DateTime data_nascimento { get; set; }
        public EnumStatus status { get; set; }




    }
}
