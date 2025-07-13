using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using API_ARMAZENA_FUNCIONARIOS.Repository.Services;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;

namespace API_ARMAZENA_FUNCIONARIOS.Services.ServiceFuncionario
{
    public class ServiceFuncionario : IServiceFuncionario
    {
        private readonly IFuncionario funcionarioRep;

        //private readonly ISetores setoresRep;

        public ServiceFuncionario(IFuncionario funcionarios)//, ISetores setores)
        {
            this.funcionarioRep = funcionarios ?? throw new ArgumentNullException(nameof(funcionarios));
            // this.setoresRep = setores ?? throw new ArgumentNullException(nameof(setores));
        }



        public async Task<List<FuncionarioResponse>?> ListarFuncionario(string nomeSetor) 
        {
            // verifico se o nome do setor é válido
            int id_Setor = await RepositorioHelper.VerificaSetor(nomeSetor);


            if (id_Setor == 0)
            {
                return null;
            }
            List<FuncionarioResponse> funcionarios = await funcionarioRep.ListarFuncionario(nomeSetor,id_Setor);
            if(funcionarios == null)
            {
                return null;
            }
            return funcionarios;
        }

        public async Task<FuncionarioResponse> PegarFuncionario(string cpf) 
        {
            if (cpf == "")
            {
                return null!;
            }

            FuncionarioResponse funcionario = await funcionarioRep.PegarFuncionario(cpf);
            if (funcionario == null)
            {
                return null!;
            }
            return funcionario;
        }


        public async Task<int> PegarIdFuncionario(string cpf) 
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return 0;
            }
            int id = await funcionarioRep.PegarIdFuncionario(cpf);
            if (id == 0)
            {
                return 0;
            }
            return id;
        }

        public async Task<bool> SalvarFuncionario(FuncionarioRequest funcionario) 
        {
            // verifico os dados que chegam
            if (funcionario == null)
            {
                return false;
            }

            // verifico se o nome do setor é válido
            int id_Setor = await RepositorioHelper.VerificaSetor(funcionario.setorNome);
            if (id_Setor == 0)
            {
                return false;
            }

            // verificar se o cpf é unico (se existe algum igual ja salvo)
            bool cpfUnico = await RepositorioHelper.CpfUniq(funcionario.cpf);
            if (!cpfUnico)
            {
                return false;
            }

            if (await funcionarioRep.SalvarFuncionario(funcionario,id_Setor))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveCliente(string cpf) 
        {
            if (cpf == "")
            {
                return false;
            }
            if (await funcionarioRep.RemoveCliente(cpf))
            {
                return true;
            }

            return false;
        }


        public async Task<bool> atualizarFunionario(int id, FuncionarioRequest funcionarioNovo)
        {
            if (id == 0)
            {
                return false;
            }

            if (await funcionarioRep.atualizarFunionario(id, funcionarioNovo))
            {
                return true;
            }
            return false;

        }
    }
}

