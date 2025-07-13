using API_ARMAZENA_FUNCIONARIOS.Model.Tables;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;

namespace API_ARMAZENA_FUNCIONARIOS.Repository.IRepository
{
    public interface ISetores
    {
        public Task<bool> SalvarSetor(SetoresRequest setor);

        public Task<bool> AtualizarSetor(string nome,SetoresRequest setorNovo, int id_setor);

        public Task<bool> RemoverSetor(string nome, int setorId);

        public Task<SetoresResponse?> PegarSetor(string nome, int idSetor);

        public Task<List<SetoresResponse>> ListarSetores();
    }
}
