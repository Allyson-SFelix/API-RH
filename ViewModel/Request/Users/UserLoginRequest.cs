using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;

namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Request.Users
{
    public class UserLoginRequest
    {
        [Required]
        public string username { get; set; } /*Username será NOME_ID do usuário*/

        [Required]
        public string senha { get; set; }

        [JsonConstructor]
        UserLoginRequest(string username, string senha)
        {
            this.username = username;
            this.senha = senha;
        }

    }
}
