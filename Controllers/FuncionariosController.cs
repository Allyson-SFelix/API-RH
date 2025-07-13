using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Records;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using API_ARMAZENA_FUNCIONARIOS.Services.ServiceFuncionario;

namespace API_ARMAZENA_FUNCIONARIOS.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionariosController : ControllerBase
    {
        /* Cada atributo relaciona com tabela do banco diferente,
         * variando também as suas funções de acessos
         */
        private readonly IServiceFuncionario serviceFuncionario;

        public FuncionariosController(IServiceFuncionario serviceFuncionario)
        {
            this.serviceFuncionario = serviceFuncionario ?? throw new ArgumentNullException(nameof(serviceFuncionario));
        }

        [Authorize]
        [HttpGet]
        [Route("listaFuncionarios")]
        public async Task<IActionResult> Get([FromBody]SetorNome value) {
            List<FuncionarioResponse>? funcionarios= await serviceFuncionario.ListarFuncionario(value.nome);
            if (funcionarios!=null )
            {
                return Ok(funcionarios); //return como json
            }
            else
            {
                return BadRequest(new { Messagem = "Lista vazia" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("unitFuncionario")]
        public async Task<IActionResult> GetUnit([FromBody] FuncionarioCpf value)
        {
            FuncionarioResponse funcionario = await serviceFuncionario.PegarFuncionario(value.cpf);
            if (funcionario != null)
            {
                return Ok(funcionario); //retorna como json
            }
            else
            {
                return BadRequest(new { mensagem = "CPF INVALIDO" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("idFuncionario")]
        public async Task<IActionResult> GetId([FromBody] FuncionarioCpf value)
        {
            int id= await serviceFuncionario.PegarIdFuncionario(value.cpf);
            if (id!=0)
            {
                return Ok(id); //retorna como json
            }
            else
            {
                return BadRequest(new { mensagem = "CPF INVALIDO" });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("inserirFuncionario")]
        public async Task<IActionResult> PostInserir([FromBody]FuncionarioRequest funcionario)
        {   
            if(await serviceFuncionario.SalvarFuncionario(funcionario) && ModelState.IsValid)
            {
                return Ok("Salvo com sucesso: Nome = "+ funcionario.nome);
            }
            return BadRequest(new { mensagem="Valor inadequados para serem salvos" });
        }

        [Authorize]
        [HttpDelete]
        [Route("removerFuncionario")]
        public async Task<IActionResult> RemoverFuncionario([FromBody]FuncionarioCpf value)
        {

            if(await serviceFuncionario.RemoveCliente(value.cpf))
            {
                return Ok("Removido com sucesso");
            }
            return BadRequest(new { Message="Remocao nao realizada" });
        }

        [Authorize]
        [HttpPut]
        [Route("atualizarFuncionario")]
        public async Task<IActionResult> PutAtualizarFuncionario([FromBody] FuncionarioRequest funcionarioNovo, int id)
        {
            if(await serviceFuncionario.atualizarFunionario(id,funcionarioNovo) && ModelState.IsValid)
            {
                return Ok(new { Message = $"Funcionario\nId: {id} \natualizado com sucesso" });
            }
            return BadRequest(new { Message = "Não foi possível realizar a atualização do usuário" });
        }
    }
}
