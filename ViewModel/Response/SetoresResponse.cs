using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;

namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Response
{
    public class SetoresResponse
    {
        public int id { get; set; }

        public string nome { get; set; }

        public int qtd_funcionarios { get; set; }

        public string localizacao { get; set; }

        public EnumStatus status { get; set; }
    }
}
