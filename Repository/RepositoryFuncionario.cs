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
using Microsoft.AspNetCore.Http.HttpResults;

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


        public async Task<List<FuncionarioResponse>> ListarFuncionario(string nomeSetor, int id_Setor)
        {

            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "SELECT f.*,s.nome AS nome_setor FROM funcionarios f " +
                        " JOIN setores s ON f.id_setor=s.id" +
                        " WHERE f.id_setor =@id  AND f.status = @status::enum_status";
                    //deve haver esse casting para infomrar que garante ser desse tipo de enum que é enum

                    List<FuncionarioResponse> funcionarios = (await conn.QueryAsync<FuncionarioResponse>(query, new { id = id_Setor, status = EnumStatus.ativo.ToString() })).ToList();
                    
                    return funcionarios;
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
            

            FuncionarioResponse funcionario = null!;

            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "SELECT f.*,s.nome AS nome_setor FROM funcionarios f" +
                        " JOIN setores s ON f.id_setor=s.id " +
                        " WHERE cpf=@Cpf AND f.status=@status::enum_status";
                    funcionario = await conn.QuerySingleAsync<FuncionarioResponse>(query, new { Cpf = cpf, status = EnumStatus.ativo.ToString() });
                    
                    return funcionario;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Pegar Funcionario: " + ex.Message);
                    return null!;
                }

            }
        }

        public async Task<bool> SalvarFuncionario([FromBody] FuncionarioRequest funcionario,int id_Setor)
        {
            try
            {

                ModelFuncionario newFuncionario = new ModelFuncionario(funcionario.nome, funcionario.dataEntrada,
                        funcionario.cpf, id_Setor, funcionario.salario, funcionario.dataNascimento, EnumStatus.ativo);

                await _connection.Funcionario.AddAsync(newFuncionario);

                await RepositorioHelper.CommitChanges(this._connection);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro em SalvarCliente: {ex.Message}");
                return false;
            }

        }


        public async Task<int> PegarIdFuncionario(string cpf)
        {
           
            
            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "SELECT id FROM funcionarios WHERE cpf=@NewCpf";

                    int id = await conn.QueryFirstAsync<int>(query, new { NewCpf = cpf });
                    
                    return id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Pegar ID Funcionario: " + ex.Message);
                    return 0;
                }
            }
        }


        public async Task<bool> RemoveCliente(string cpf)
        {
            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "UPDATE funcionarios SET status=@status::enum_status WHERE cpf=@cpfEntrada";
                    await conn.QueryAsync(query, new { cpfEntrada = cpf, status = EnumStatus.nao_ativo.ToString() });
                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                    return false;
                }
            }

        }
        public async Task<bool> atualizarFunionario(int id, FuncionarioRequest funcionarioNovo)
        {
            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "UPDATE funcionarios" +
                        " SET nome=@NovoNome,cpf=@NovoCPF,dataentrada=@NovaDataEntrada,id_setor=@setorID,salario=@NovoSalario,data_nascimento=@NovaDataNascimento" +
                        " WHERE id=@idRecebido";

                    await conn.QueryAsync(query, new
                    {
                        NovoNome = funcionarioNovo.nome,
                        NovoCpf = funcionarioNovo.cpf,
                        NovaDataEntrada = funcionarioNovo.dataEntrada.ToDateTime(TimeOnly.MinValue),
                        setorID = id,
                        NovoSalario = funcionarioNovo.salario,
                        NovaDataNascimento = funcionarioNovo.dataNascimento.ToDateTime(TimeOnly.MinValue),
                        idRecebido = id
                    });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                    return false;
                }
            }
        }
    }
}
