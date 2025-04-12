using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;

namespace API_ARMAZENA_FUNCIONARIOS.Repository.IRepository
{
    public interface IFuncionario
    {
        public Task<List<FuncionarioResponse>> ListarFuncionario(string nomeSetor);
        public Task<FuncionarioResponse> PegarFuncionario(string cpf);
        public Task<bool> SalvarFuncionario(FuncionarioRequest cliente);
        public Task CommitChanges();
        // public bool RemoveCliente(int id);
        //public bool AtualizaCliente(int id);
        //public bool AtualizarDadoCliente(int id,string opcao);
    }
}
