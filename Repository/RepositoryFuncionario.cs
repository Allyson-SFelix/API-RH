using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionContext;
using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;
using Microsoft.EntityFrameworkCore;
using API_ARMAZENA_FUNCIONARIOS.Model.Tables;
using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionDapper;
using Dapper;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;


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


        public async Task<List<FuncionarioResponse>> ListarFuncionario() {
            var clientes = await _connection.Funcionario.ToListAsync();
            
            if (clientes == null)
            {
                return null;
            }
            
            try
            {
                List<FuncionarioResponse> clienteOficial = clientes.Select(cl => new FuncionarioResponse(cl.nome, cl.idade, cl.id)).ToList();

                return clienteOficial;
            }
            catch(Exception ex) {
                Console.WriteLine("Erro no ListarClientesFisica" + ex.Message);
                return null;
            }
        }
        public async Task<FuncionarioResponse> PegarFuncionario(int id)
        {
            var cliente = await _connection.Funcionario.FirstOrDefaultAsync(cl=>cl.id==id);
            if (cliente != null)
            {
                FuncionarioResponse oficialCliente = new FuncionarioResponse(cliente.nome, cliente.idade, cliente.id);
                return oficialCliente;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> SalvarFuncionario(FuncionarioRequest funcionario)
        {
            // verifico os dados que chegam
            if (funcionario == null)
            {
                return false;
            }

            // verifico se o nome do setor é válido
            int id_Setor = 0;
            using (var connection = DbConennectionDapper.GetStringConnection())
            {
                string query = "SELECT id FROM setor WHERE nome=@nome";
                var resultQuery= await connection.QuerySingleAsync<int>(query, new {nome = funcionario.setorNome});
                id_Setor = resultQuery;
                if (id_Setor == 0)
                {
                    return false;
                }
            }



                try
                {

                    ModelFuncionario newCliente = new ModelFuncionario(funcionario.nome,funcionario.dataEntrada,
                        funcionario.cpf,id_Setor,funcionario.salario,funcionario.dataNascimento,EnumStatus.ativo);
                    _connection.Funcionario.Add(newCliente);
                    await CommitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro em SalvarCliente: {ex.Message}");
                    return false;
                }

        }

        public async Task CommitChanges()
        {
            await _connection.SaveChangesAsync();
        }
        //public bool RemoveCliente(int id) { }
        // public bool AtualizaCliente(int id) { }
        //public bool AtualizarDadoCliente(int id, string opcao) { }
    }
}
