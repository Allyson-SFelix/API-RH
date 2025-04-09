using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;

namespace API_ARMAZENA_FUNCIONARIOS.Model.Tables
{
    [Table("setores")]
    public class ModelSetores
    {
        [Key]
        public int id { get; set; }

        public string nome { get; set; }

        public int qtd_funcionarios { get; set; }

        public string localizacao { get; set; }

        public EnumStatus status { get; set; }
    }
}
