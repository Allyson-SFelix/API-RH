using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;

namespace API_ARMAZENA_FUNCIONARIOS.Model.Tables
{
    [Table("users")]
    public class ModelUsers
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        public string senha_hash { get; set; }

        public EnumStatus status { get; set; }

        public EnumRoles roles { get; set; }
    }
}
}
