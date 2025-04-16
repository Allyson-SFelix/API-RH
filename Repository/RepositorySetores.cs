using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionContext;
using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;
using Microsoft.EntityFrameworkCore;
using API_ARMAZENA_FUNCIONARIOS.Model.Tables;
using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionDapper;
using Dapper;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;
using API_ARMAZENA_FUNCIONARIOS.Repository.Services;
using Microsoft.AspNetCore.Mvc;
namespace API_ARMAZENA_FUNCIONARIOS.Repository
{
    public class RepositorySetores : ISetores
    {
        private DbConnectionContext _connection;

        public RepositorySetores( DbConnectionContext connection)
        {
            this._connection = connection;
        }


        public async Task<bool> SalvarSetor(SetoresRequest setor)
        {
            if (setor == null)
            {
                return false;
            }

            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                string sql = "SELECT nome FROM setores WHERE nome=@nome" ;
                var result = await conn.QueryFirstOrDefaultAsync<string>(sql, new { nome = setor.nome });
                if (result == null)
                {
                    return false;
                }
            };

            /*problema nesse bloco*/
            try
            {
                ModelSetores setorModel = new ModelSetores(setor.nome, setor.qtd_funcionarios, setor.localizacao, EnumStatus.ativo.ToString());
                await _connection.Setores.AddAsync(setorModel);
                await ServicesRepository.CommitChanges(this._connection);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Salvar Setor: " + ex.Message);
                return false;
            }

        }
        public Task<bool> AtualizarSetor(string nome) 
        {
            return null!;
        }

        public Task<bool> RemoverSetor(string nome) 
        {
            return null!;
        }

        public async Task<SetoresResponse> PegarSetor(string nome) 
        {
            if(nome == null)
            {
                return null!;
            }

            int idSetor =await ServicesRepository.VerificaSetor(nome);
            try
            {
                using (var conn = DbConennectionDapper.GetStringConnection())
                {
                    string sql = "SELECT nome,qtd_funcionarios AS quantidade_funcionarios,localizacao,status FROM setores WHERE id=@id";

                    SetoresResponse setoreResult = await conn.QueryFirstAsync<SetoresResponse>(sql, new { id = idSetor });
                    if (setoreResult == null)
                    {
                        return null!;
                    }
                    return setoreResult;

                };
            }catch(Exception ex)
            {
                Console.WriteLine("Pegar Setor: " + ex.Message);
                return null!;
            }

        }

    }
}
