using System.Text;
using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionContext;
using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionDapper;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;
using API_ARMAZENA_FUNCIONARIOS.Repository;
using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using API_ARMAZENA_FUNCIONARIOS.Services.ServicesUsersLogin;
using API_ARMAZENA_FUNCIONARIOS.Services.TokenAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
/*
"JwtSettings": {
    "SecretKey": "", //é uma var secret ou ambiente 
            "Issuer": "RH-API", // de onde vem o token
            "Audience": "Users", // quem pode usar esse token
            "ExpirationMinutes": "15" //tempo de expiração do token
   }
*/
/* SOBRE JWT
//dotnet user-secrets set "JwtSettings:SecretKey" "Chave"
// pego a local
var KeySecret =builder.Configuration["JwtSettings:SecretKey"];
// rescrevo no json com a chave sem expor
builder.Configuration["JwtSettings:SecretKey"] = KeySecret;
*/





// pegar do user-secret a string de conexão
//dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=IP;Port=Port;Database=BancoNome;User Id=User;Password=Senha;"
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// pegar key no user-secret
var KeySecret = builder.Configuration["JwtSettings:SecretKey"];

KeyToken.SalvarKey(KeySecret);

//armazenando para injeção de dependencia da string de conexão
builder.Services.AddDbContext<DbConnectionContext>(options =>
    options.UseNpgsql(connectionString,
    o =>
    { //cadastrando os enums para serem semelhantes no uso
        o.MapEnum<EnumStatus>();
        o.MapEnum<EnumRoles>();
    } ) ); 




// passo a string de conexao para a classe do Dapper
DbConennectionDapper.SetConnection(connectionString!);


// injeção que toda vez que for acionado será instanciado um novo objeto dessa classe que utiliza essa interface
builder.Services.AddScoped<IFuncionario,RepositoryFuncionario>();
builder.Services.AddScoped<ISetores,RepositorySetores>();
builder.Services.AddScoped<IRepositoryUsersLogin, RepositoryUsers>();
builder.Services.AddScoped<IUsersLoginService, UsersLoginService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var chave = Encoding.UTF8.GetBytes(KeyToken.secretKey);

// Configurar autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            // ValidIssuer = "minhaapi",
            // ValidAudience = "meusclientes",
            IssuerSigningKey = new SymmetricSecurityKey(chave)
        };
    });


builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger();
}

// Permite qualquer um acessar qualquer metodo e de qualquer origem 
app.UseCors(builder =>
        builder.AllowAnyOrigin()  // Permite todas as origens ou builder.WithOrigins("https://meudominio.com") 
               .AllowAnyMethod()  // Permite qualquer método 
               .AllowAnyHeader() // Permite qualquer cabecalho (tipo de conteudo,token, ...)
);



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
