using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;

namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Request.Users
{
    public class UserRegisterRequest
    {
        [Required]
        public string username { get; set; } /*Username será NOME_ID do usuário*/

        [Required]
        public string senha { get; set; }

        public EnumRoles role { get; set; }

        [JsonConstructor]
        public UserRegisterRequest(string username, string senha, EnumRoles role)
        {
            this.username = username;
            this.senha = senha;
            this.role = role;
        }
    }
}
