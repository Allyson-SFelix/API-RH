using Microsoft.EntityFrameworkCore;
using API_ARMAZENA_FUNCIONARIOS.Model.Tables;


namespace API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionContext
{
    public class DbConnectionContext : DbContext
    {
        // cada atributo relaciona com o acesso pelo Entity com o banco de dados e sua respectiva tabela
        public DbSet<ModelFuncionario> Funcionario { get; set;}
        public DbSet<ModelSetores> Setores { get; set; }
        public DbSet<ModelUsers> Users { get; set; }
        public DbSet<ModelLogs> Logs { get; set; }



        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"Server=IP;Port=PORT;Database=NOMEBANCO;User Id=ID;Password=SENHA;");
        }*/

        public DbConnectionContext(DbContextOptions<DbConnectionContext> options)
        : base(options) { }
    }
}

