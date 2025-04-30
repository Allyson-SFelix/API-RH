using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;

namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Response
{
    public class SetoresResponse
    {
        public int id { get; set; }

        public string? nome { get; set; }

        public int qtd_funcionarios { get; set; }

        public string? localizacao { get; set; }

//        public EnumStatus status { get; set; }  -> VAI HAVER APENAS A VERIFICACAO DO STATUS NO MOMENTO DA BUSCA,EDICAO E ALTERACAO NA REMOCAO

        public SetoresResponse() { }
        public SetoresResponse(int id, string nome, int qtd_funcionarios, string localizacao)
        {
            this.id = id;
            this.nome = nome;
            this.qtd_funcionarios = qtd_funcionarios;
            this.localizacao = localizacao;
        }
    }
}
