using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;

namespace API_ARMAZENA_FUNCIONARIOS.Model.Tables
{
    [Table("movimentacao_users")]
    public class ModelLogs
    {
        [Key]
        public int id { get; set; }
        public DateTime data_modificacao { get; set; }

        public string dados_anterior { get; set; }

        public string atos_efetuados { get; set; }

        public string tabela_afetada { get; set; }

        public int id_users { get; set; }

    }
}
