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
using Microsoft.IdentityModel.Tokens;
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


            if (await ServicesRepository.VerificarSetorExiste(setor.nome))
            {
                return false;
            }

            try
            {
               if (setor.qtd_funcionarios < 0)
               {
                   return false;
               }
               ModelSetores setorModel = new ModelSetores(setor.nome, setor.qtd_funcionarios, setor.localizacao, EnumStatus.ativo);

                
                await _connection.Setores.AddAsync(setorModel);

                
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
            if(nome == "")
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
            if (nome == "")
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

        public async Task<bool> RemoverSetor(string nome) 
        {
            bool setorExiste = (await ServicesRepository.VerificarSetorExiste(nome));

            if (nome == "" || !setorExiste)
            {
                return false;
            }

            int setorId = await ServicesRepository.VerificaSetor(nome);
            if(setorId == 0 )
            {
                return false;
            }

            if (await ServicesRepository.QtdFuncionariosSetor(setorId)==-1)
            {
                return false;
            }
            

            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "UPDATE setores SET status=@statusNovo::enum_status"+ 
                                   " WHERE id=@idSetor AND status=@status::enum_status ";
                    await conn.QueryAsync(query,new {statusNovo= EnumStatus.nao_ativo.ToString(), idSetor=setorId , status=EnumStatus.ativo.ToString()});
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: "+e.Message);
                    return false;
                }
            }


        }


        public async Task<List<SetoresResponse>> ListarSetores()
        {
            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "SELECT * FROM setores ORDER BY id ASC";
                    List<SetoresResponse> lista = (await conn.QueryAsync<SetoresResponse>(query)).ToList();
                    if(lista == null)
                    {
                        return null!;
                    }
                    return lista;

                }catch(Exception e)
                {
                    Console.WriteLine("Exception: "+e.Message);
                    return null!;
                }
            }
        }

    }
}
