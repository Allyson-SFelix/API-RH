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

        public string username { get; set; }

        public string senha_hash { get; set; }

        public string salt { get; set; }

        public EnumStatus status { get; set; }

        public EnumRoles roles { get; set; }


        public ModelUsers() { }
        public ModelUsers(string username, string senha_hash, string salt, EnumStatus status, EnumRoles roles)
        {
            this.username = username;
            this.senha_hash = senha_hash;
            this.salt = salt;
            this.status = status;
            this.roles = roles;
        }
    }
}
