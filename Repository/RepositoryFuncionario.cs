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


        public async Task<List<FuncionarioResponse>> ListarFuncionario(string nomeSetor)
        {
            // verifico se o nome do setor é válido
            int id_Setor = await ServicesRepository.VerificaSetor(nomeSetor);

            if (id_Setor == 0)
            {
                return null!;
            }

            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "SELECT f.*,s.nome AS nome_setor FROM funcionarios f " +
                        " JOIN setores s ON f.id_setor=s.id" +
                        " WHERE f.id_setor =@id  AND f.status = @status::enum_status";
                    //deve haver esse casting para infomrar que garante ser desse tipo de enum que é enum

                    List<FuncionarioResponse> funcionarios = (await conn.QueryAsync<FuncionarioResponse>(query, new { id = id_Setor, status = EnumStatus.ativo.ToString() })).ToList();
                    if (funcionarios == null)
                    {
                        return null!;
                    }
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
            if (cpf == "")
            {
                return null!;
            }


            FuncionarioResponse funcionario = null!;

            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "SELECT f.*,s.nome AS nome_setor FROM funcionarios f" +
                        " JOIN setores s ON f.id_setor=s.id " +
                        " WHERE cpf=@Cpf AND f.status=@status::enum_status";
                    funcionario = await conn.QuerySingleAsync<FuncionarioResponse>(query, new { Cpf = cpf, status = EnumStatus.ativo.ToString() });
                    if (funcionario == null)
                    {
                        return null!;
                    }
                    return funcionario;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Pegar Funcionario: " + ex.Message);
                    return null!;
                }

            }
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
            if (id_Setor == 0)
            {
                return false;
            }

            // verificar se o cpf é unico (se existe algum igual ja salvo)
            bool cpfUnico = await ServicesRepository.CpfUniq(funcionario.cpf);
            if (!cpfUnico)
            {
                return false;
            }

            try
            {

                ModelFuncionario newFuncionario = new ModelFuncionario(funcionario.nome, funcionario.dataEntrada,
                        funcionario.cpf, id_Setor, funcionario.salario, funcionario.dataNascimento, EnumStatus.ativo);

                await _connection.Funcionario.AddAsync(newFuncionario);

                await ServicesRepository.CommitChanges(this._connection);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro em SalvarCliente: {ex.Message}");
                return false;
            }

        }


        public async Task<bool> RemoveCliente(string cpf)
        {
            if (cpf == "")
            {
                return false;
            }

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
        public async Task<bool> atualizarFunionario(string cpf, FuncionarioRequest funcionarioNovo)
        {
            if (cpf == "")
            {
                return false;
            }

            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "UPDATE funcionarios" +
                        " SET nome=@NovoNome,cpf=@NovoCPF,dataentrada=@NovaDataEntrada,id_setor=@setorID,salario=@NovoSalario,data_nascimento=@NovaDataNascimento" +
                        " WHERE cpf=@cpfRecebido";
                    int setorId = await ServicesRepository.VerificaSetor(funcionarioNovo.setorNome);

                    string novoCpf = funcionarioNovo.cpf;

                    if (setorId == 0)
                    {
                        return false;
                    }

                    if (cpf == funcionarioNovo.cpf)
                    {
                        novoCpf = cpf;
                    }
                    else if (!(await ServicesRepository.CpfUniq(funcionarioNovo.cpf)))
                    {
                        return false;
                    }
                    else
                    {
                        novoCpf = funcionarioNovo.cpf;
                    }

                    await conn.QueryAsync(query, new
                    {
                        NovoNome = funcionarioNovo.nome,
                        NovoCpf = novoCpf,
                        NovaDataEntrada = funcionarioNovo.dataEntrada,
                        setorID = setorId,
                        NovoSalario = funcionarioNovo.salario,
                        NovaDataNascimento = funcionarioNovo.dataNascimento,
                        cpfRecebido = cpf
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
