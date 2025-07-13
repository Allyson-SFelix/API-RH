using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;

namespace API_ARMAZENA_FUNCIONARIOS.Services.ServiceFuncionario
{
    public interface IServiceFuncionario
    {

        public Task<List<FuncionarioResponse>?> ListarFuncionario(string nomeSetor);

        public Task<FuncionarioResponse> PegarFuncionario(string cpf);


        public Task<int> PegarIdFuncionario(string cpf);

        public Task<bool> SalvarFuncionario(FuncionarioRequest funcionario);

        public Task<bool> RemoveCliente(string cpf);


        public Task<bool> atualizarFunionario(int id, FuncionarioRequest funcionarioNovo);
    }
}
