using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_ARMAZENA_FUNCIONARIOS.Model.Tables;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API_ARMAZENA_FUNCIONARIOS.Services.TokenAuth
{
    public static class ConfigurationToken
    {

        public static string GenerationToken(ModelUsers user)
        {
            //cria o handler
            var handler = new JwtSecurityTokenHandler();

            // array de bytes
            var keySecurity = Encoding.UTF8.GetBytes(KeyToken.secretKey);

            //assinatura token
            var credentials = new SigningCredentials(new SymmetricSecurityKey(keySecurity), SecurityAlgorithms.HmacSha256Signature);


            // criação dos claims / informações do usuário
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.username), //username
                new Claim(ClaimTypes.Role, user.roles.ToString())  // função
            };

            // descricao do token
            var descricaoToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = credentials,

//                Issuer = "minhaapi",
//                Audience = "meusclientes"
            };


            // especifica o que tem no token
            var token = handler.CreateToken(descricaoToken);
        
            // gera um token em string
            return handler.WriteToken(token);
        }
    }
}
