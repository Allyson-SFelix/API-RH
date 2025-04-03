using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API_ARMAZENA_FUNCIONARIOS.Model
{
    [Table("clientelojafisica")]
    public class ModelFuncionario
    {
        [Key]
        public int id { get; private set; }
        public string nome { get; set; } 

        public int idade {  get;  set; }


        public ModelFuncionario(string nome, int idade)
        {
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
            this.idade = idade;
        }
        


    }
}
