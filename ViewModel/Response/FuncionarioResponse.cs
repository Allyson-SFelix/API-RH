namespace API_ARMAZENA_FUNCIONARIOS.ViewModel.Response
{
    public class FuncionarioResponse
    {

        public string? nome { get; set; }
        public DateTime dataEntrada { get; set; }
        public string? cpf { get; set; }
        public float salario { get; set; }
        public DateTime data_nascimento { get; set; }

      

        // possíveis retornos serão Nome e Idade,sem necessidade para ID na prática
        // ID usado apenas para teste

    }
}

