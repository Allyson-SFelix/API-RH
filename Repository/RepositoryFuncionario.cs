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
    /* REPOSITORY
     * armazena o contexto do banco
     * realiza o acesso ao banco
     * implmenta sua determinada regra de negocio
     */
    public class RepositoryFuncionario : IFuncionario
    {
        private readonly DbConnectionContext _connection;

        public RepositoryFuncionario(DbConnectionContext connection)
        {
            this._connection = connection;
        }


        public async Task<List<FuncionarioResponse>> ListarFuncionario(string nomeSetor)
        {
            // verifico se o nome do setor é válido
            int id_Setor = await ServicesRepository.VerificaSetor(nomeSetor);

            if (id_Setor == 0)
            {
                return null!;
            }

            List<FuncionarioResponse> listaFuncionariosResponse = new List<FuncionarioResponse>();
            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "SELECT f.* FROM funcionarios  f" +
                        " JOIN setores s ON f.id_setor = s.id" +
                        " WHERE id_setor =@id  AND f.status='ativo'";

                    var funcionarios = await conn.QueryAsync<FuncionarioResponse>(query,new {id=id_Setor});
                    if (funcionarios != null)
                    {
                        foreach(FuncionarioResponse f in funcionarios){
                            listaFuncionariosResponse.Add(f);
                        }
                    }
                    return listaFuncionariosResponse;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Listar Funcionarios: " + ex.Message);
                    return null!;
                }

            }
        }

        public async Task<FuncionarioResponse> PegarFuncionario(string cpf)
        {
            if (cpf == "")
            {
                return null;
            }


            FuncionarioResponse  funcionario = null!;
            
            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "SELECT f.*, s.nome AS setor_nome FROM funcionarios" +
                        "JOIN setores s ON f.id_setor = f.id" +
                        " WHERE cpf=@Cpf AND status=@status";
                    funcionario = await conn.QuerySingleAsync<FuncionarioResponse>(query, new { Cpf = cpf, status = EnumStatus.ativo });
                    if (funcionario == null)
                    {
                        return null;
                    }
                }
                catch(Exception ex) 
                {
                    Console.WriteLine("Pegar Funcionario: "+ex.Message); 
                    return null;
                }
            
            }
            return funcionario;
        }

        public async Task<bool> SalvarFuncionario([FromBody] FuncionarioRequest funcionario)
        {  
            // verifico os dados que chegam
            if (funcionario == null)
            {
                return false;
            }

            // verifico se o nome do setor é válido
            int id_Setor = await ServicesRepository.VerificaSetor(funcionario.setorNome);

                try
                {

                    ModelFuncionario newFuncionario = new ModelFuncionario(funcionario.nome,funcionario.dataEntrada,
                        funcionario.cpf,id_Setor,funcionario.salario,funcionario.dataNascimento,EnumStatus.ativo);
                    
                    await  _connection.Funcionario.AddAsync(newFuncionario);
                    
                    await ServicesRepository.CommitChanges(this._connection);
                    
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro em SalvarCliente: {ex.Message}");
                    Console.WriteLine("Erro ao salvar no banco: " + ex.InnerException?.Message);
                     return false;
                }

        }

        
        //public bool RemoveCliente(int id) { }
        // public bool AtualizaCliente(int id) { }
        //public bool AtualizarDadoCliente(int id, string opcao) { }
    }
}
