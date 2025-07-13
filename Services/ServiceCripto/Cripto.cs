using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace API_ARMAZENA_FUNCIONARIOS.Services.ServiceCripto
{
    public static class Cripto
    {
        /// <summary>
        /// Função: recebo senha digitada pelo usuário e gero o hash da senha e salvo o salt(numero aleatório gerado com 16bytes)
        /// </summary>
        /// <param name="GerarCriptoSenha">Gerador CriptoSenha</param>
        /// <returns> novoHash e salt(numero aleatório em string gerado no momento) </returns>
        public static (string novoHash,string salt) GerarCriptoSenha(string senha)
        {
            
             byte[] saltByte = RandomNumberGenerator.GetBytes(16); // 128 bits 
             string salt = Convert.ToBase64String(saltByte); //converte em string o salt gerado
            

            string novoHash = Convert.ToBase64String(KeyDerivation.Pbkdf2( //converte em string o hash gerado pelo KeyDerivation
                    password: senha, //senha digitada
                    salt: saltByte, // numero aleatório gerado
                    prf: KeyDerivationPrf.HMACSHA256,  // usa a senha e o salt para gerar uma derivação (função hash)
                    iterationCount: 60000, // quantidade de vezes que o hash é executado
                    numBytesRequested: 256 / 8)); // tamanho do hash final (32 bytes)

            return (novoHash,salt);
        }


        /// <summary>
        /// Função: recebo senha digitada pelo usuário, hash do banco do mesmo usuário e o seu salt. Dessa forma, coverto a senha com o mesmo salt e comparo os hash's
        /// </summary>
        /// <param name="VerificarSenhaCripto">Verifica CriptoSenha</param>
        /// <returns> true para senha certa e false para senha errada </returns>
        public static bool VerificarSenhaCripto(string senhaDigitada, string hashSalvo, string saltSalvo)
        {
            byte[] salt = Convert.FromBase64String(saltSalvo);

            string novoHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senhaDigitada,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 60000,
                numBytesRequested: 32));

            return hashSalvo == novoHash;
        }
    }
}
