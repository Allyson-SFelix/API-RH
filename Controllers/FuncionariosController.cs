using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;

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
        [Route("/listaFuncionarios")]
        public async Task<IActionResult> Get(string nomeSetor) {
            List<FuncionarioResponse> funcionarios= await funcionarioRep.ListarFuncionario(nomeSetor);
            if (funcionarios.Count() >0)
            {
                return Ok(funcionarios); //return como json
            }
            else
            {
                return BadRequest(new { Messagem = "Lista vazia" });
            }
        }

        [HttpGet]
        [Route("/unitFuncionario")]
        public async Task<IActionResult> GetUnit(string cpf)
        {
            FuncionarioResponse funcionario = await funcionarioRep.PegarFuncionario(cpf);
            if (funcionario != null)
            {
                return Ok(funcionario); //retorna como json
            }
            else
            {
                return BadRequest(new { mensagem = "CPF INVALIDO" });
            }
        } 

        [HttpPost]
        [Route("/inserirFuncionario")]
        public async Task<IActionResult> PostInserir([FromBody]FuncionarioRequest funcionario)
        {   

            if(await funcionarioRep.SalvarFuncionario(funcionario))
            {
                return Ok("Salvo com sucesso: Nome = "+ funcionario.nome);
            }
            return BadRequest(new { mensagem="Valor inadequados para serem salvos" });
        }


        [HttpDelete]
        [Route("/removerFuncionario")]
        public async Task<IActionResult> RemoverFuncionario(string cpf)
        {
            if(await funcionarioRep.RemoveCliente(cpf))
            {
                return Ok("Removido com sucesso");
            }
            return BadRequest(new { Message="Remocao nao realizada" });
        }
    }
}
