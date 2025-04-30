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
using System.Diagnostics;
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


            bool statusSetor = await ServicesRepository.VerificarSetorExiste(setor.nome);
            if (!statusSetor)
            {
                return false;
            }

            try
            {
               Debug.WriteLine("dentro try");
               ModelSetores setorModel = new ModelSetores(setor.nome, setor.qtd_funcionarios, setor.localizacao, EnumStatus.ativo);

                Debug.WriteLine("add no banco");
                await _connection.Setores.AddAsync(setorModel);

                Debug.WriteLine("commit no banco");
                await ServicesRepository.CommitChanges(this._connection);
                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Salvar Setor: " + ex.Message);
                return false;
            }

        }
        public async Task<SetoresResponse> PegarSetor(string nome) 
        {
            if(nome == null)
            {
                return null!;
            }

            int idSetor =await ServicesRepository.VerificaSetor(nome);
            if (idSetor == 0)
            {
                return null!;
            }
            try
            {
                using (var conn = DbConennectionDapper.GetStringConnection())
                {
                    string sql = "SELECT id,nome,qtd_funcionarios,localizacao FROM setores"+ 
                                 " WHERE id=@id AND status=@status::enum_status";

                    var setorResult = await conn.QueryFirstOrDefaultAsync<SetoresResponse>(sql, new { id = idSetor , status=EnumStatus.ativo.ToString()});
                    if (setorResult == null)
                    {
                        return null!;
                    }
                    return setorResult;

                };
            }catch(Exception ex)
            {
                Console.WriteLine("Pegar Setor: " + ex.Message);
                return null!;
            }

        }
        public async Task<bool> AtualizarSetor(string nome,SetoresRequest setorNovo) 
        {
            if (nome == null)
            {
                return false;
            }

            int id_setor = await ServicesRepository.VerificaSetor(nome);
            if(id_setor == 0 && !(await ServicesRepository.VerificarSetorExiste(nome)) )
            {
                return false;
            }
            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "UPDATE setores SET nome=@nomeNovo , qtd_funcionarios=@qtdFunc, localizacao=@local" +
                    " WHERE id=@id";

                    await conn.QueryAsync(query, new { id = id_setor, nomeNovo = setorNovo.nome, qtdFunc = setorNovo.qtd_funcionarios, local = setorNovo.localizacao });
                    
                    return true;
                
                }catch (Exception ex) 
                { 
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public Task<bool> RemoverSetor(string nome) 
        {
            return null!;
        }


    }
}
