using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;

namespace API_ARMAZENA_FUNCIONARIOS.Repository.IRepository
{
    public interface ISetores
    {
        public Task<bool> SalvarSetor(SetoresRequest setor);

        public Task<bool> AtualizarSetor(string nome);

        public Task<bool> RemoverSetor(string nome);

        public Task<SetoresResponse> PegarSetor(string nome);
    }
}
