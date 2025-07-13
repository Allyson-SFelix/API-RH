using System.Text;
using Microsoft.VisualBasic;

namespace API_ARMAZENA_FUNCIONARIOS.Services.TokenAuth
{
    public class KeyToken
    {
        public static string secretKey { get; private set; } = string.Empty;
    
        public static void SalvarKey(string secretKeyE)
        {
            secretKey = secretKeyE;
        }
        
    }
}
