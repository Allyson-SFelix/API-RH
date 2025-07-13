using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using API_ARMAZENA_FUNCIONARIOS.Repository.Services;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;

namespace API_ARMAZENA_FUNCIONARIOS.Services.ServiceSetores
{
    public class ServiceSetores:IServiceSetores
    {
        private readonly ISetores setoresRep;

        public ServiceSetores(ISetores setoresRep)
        {
            this.setoresRep = setoresRep;
        }
        public async Task<bool> SalvarSetor(SetoresRequest setor)
        {
            if (setor == null)
            {
                return false;
            }


            if (await RepositorioHelper.VerificarSetorExiste(setor.nome))
            {
                return false;
            }

            if(await setoresRep.SalvarSetor(setor))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AtualizarSetor(string nome, SetoresRequest setorNovo)
        {
            if (nome == "")
            {
                return false;
            }

            int id_setor = await RepositorioHelper.VerificaSetor(nome);
            if (id_setor == 0 && !(await RepositorioHelper.VerificarSetorExiste(nome)))
            {
                return false;
            }

            if (await setoresRep.AtualizarSetor(nome, setorNovo,id_setor))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> RemoverSetor(string nome)
        {
            bool setorExiste = (await RepositorioHelper.VerificarSetorExiste(nome));

            if (nome == "" || !setorExiste)
            {
                return false;
            }

            int setorId = await RepositorioHelper.VerificaSetor(nome);
            if (setorId == 0)
            {
                return false;
            }

            if (await RepositorioHelper.QtdFuncionariosSetor(setorId) == -1)
            {
                return false;
            }


            if (await setoresRep.RemoverSetor(nome,setorId))
            {
                return true;
            }
            return false;

        }

        public async Task<SetoresResponse?> PegarSetor(string nome)
        {

            if (nome == "")
            {
                return null!;
            }

            int idSetor = await RepositorioHelper.VerificaSetor(nome);
            if (idSetor == 0)
            {
                return null!;
            }
            return await setoresRep.PegarSetor(nome,idSetor);
        }

        public async Task<List<SetoresResponse>> ListarSetores()
        {
            List<SetoresResponse> lista = await setoresRep.ListarSetores();
            if (lista == null)
            {
                return null!;
            }
            return lista;
        }
    }
}
