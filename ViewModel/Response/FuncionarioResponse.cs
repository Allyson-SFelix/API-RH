namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Response
{
    public class FuncionarioResponse
    {
            public int id { get; set; }
            public string nome { get; set; }
            public int idade { get; set; }

            // possíveis retornos serão Nome e Idade,sem necessidade para ID na prática
            // ID usado apenas para teste
            public FuncionarioResponse(string nome)
            {
                this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
            }
            public FuncionarioResponse(string nome, int idade)
            {
                this.idade = idade;
                this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
            }
            public FuncionarioResponse(string nome, int idade,int id)
            {
                this.idade = idade;
                this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
                this.id = id;
            }
    }
}

