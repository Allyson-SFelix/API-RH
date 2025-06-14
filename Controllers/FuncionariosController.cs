using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Records;
using Microsoft.IdentityModel.Tokens;

namespace API_ARMAZENA_FUNCIONARIOS.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionariosController : ControllerBase
    {
        /* Cada atributo relaciona com tabela do banco diferente,
         * variando também as suas funções de acessos
         */
        private readonly IFuncionario funcionarioRep;

        private readonly ISetores setoresRep;

        public FuncionariosController(IFuncionario funcionarios, ISetores setores)
        {
            this.funcionarioRep = funcionarios ?? throw new ArgumentNullException(nameof(funcionarios));
            this.setoresRep = setores ?? throw new ArgumentNullException(nameof(setores));
        }

        [HttpGet]
        [Route("listaFuncionarios")]
        public async Task<IActionResult> Get([FromBody]SetorNome value) {
            List<FuncionarioResponse> funcionarios= await funcionarioRep.ListarFuncionario(value.nome);
            if (funcionarios!=null )
            {
                return Ok(funcionarios); //return como json
            }
            else
            {
                return BadRequest(new { Messagem = "Lista vazia" });
            }
        }

        [HttpGet]
        [Route("unitFuncionario")]
        public async Task<IActionResult> GetUnit([FromBody] FuncionarioCpf value)
        {
            FuncionarioResponse funcionario = await funcionarioRep.PegarFuncionario(value.cpf);
            if (funcionario != null)
            {
                return Ok(funcionario); //retorna como json
            }
            else
            {
                return BadRequest(new { mensagem = "CPF INVALIDO" });
            }
        }

        [HttpGet]
        [Route("idFuncionario")]
        public async Task<IActionResult> GetId([FromBody] FuncionarioCpf value)
        {
            int id= await funcionarioRep.PegarIdFuncionario(value.cpf);
            if (id!=0)
            {
                return Ok(id); //retorna como json
            }
            else
            {
                return BadRequest(new { mensagem = "CPF INVALIDO" });
            }
        }

        [HttpPost]
        [Route("inserirFuncionario")]
        public async Task<IActionResult> PostInserir([FromBody]FuncionarioRequest funcionario)
        {   
            if(await funcionarioRep.SalvarFuncionario(funcionario) && ModelState.IsValid)
            {
                return Ok("Salvo com sucesso: Nome = "+ funcionario.nome);
            }
            return BadRequest(new { mensagem="Valor inadequados para serem salvos" });
        }


        [HttpDelete]
        [Route("removerFuncionario")]
        public async Task<IActionResult> RemoverFuncionario([FromBody]FuncionarioCpf value)
        {

            if(await funcionarioRep.RemoveCliente(value.cpf))
            {
                return Ok("Removido com sucesso");
            }
            return BadRequest(new { Message="Remocao nao realizada" });
        }


        [HttpPut]
        [Route("atualizarFuncionario")]
        public async Task<IActionResult> PutAtualizarFuncionario([FromBody] FuncionarioRequest funcionarioNovo, int id)
        {
            if(await funcionarioRep.atualizarFunionario(id,funcionarioNovo) && ModelState.IsValid)
            {
                return Ok(new { Message = $"Funcionario\nId: {id} \natualizado com sucesso" });
            }
            return BadRequest(new { Message = "Não foi possível realizar a atualização do usuário" });
        }
    }
}
