using Microsoft.EntityFrameworkCore;
using API_ARMAZENA_FUNCIONARIOS.Model;


namespace API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionContext
{
    public class DbConnectionContext : DbContext
    {
        public DbSet<ModelFuncionario> Funcionario { get; set;}

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"Server=IP;Port=PORT;Database=NOMEBANCO;User Id=ID;Password=SENHA;");
        }*/

        public DbConnectionContext(DbContextOptions<DbConnectionContext> options)
        : base(options) { }
    }
}

