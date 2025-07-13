using API_ARMAZENA_FUNCIONARIOS.Services.ServiceSetores;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;

namespace API_ARMAZENA_FUNCIONARIOS.Repository.IRepository
{
    public interface IFuncionario
    {
        public Task<List<FuncionarioResponse>> ListarFuncionario(string nomeSetor, int id_Setor);
        public Task<FuncionarioResponse> PegarFuncionario(string cpf);
        public Task<bool> SalvarFuncionario(FuncionarioRequest funcionario,int id_Setor);
        public Task<bool> RemoveCliente(string cpf);
        public Task<int> PegarIdFuncionario(string cpf);
        public Task<bool> atualizarFunionario(int id, FuncionarioRequest funcionarioNovo);
    }
}
