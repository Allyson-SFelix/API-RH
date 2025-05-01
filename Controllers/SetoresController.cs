using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;
using API_ARMAZENA_FUNCIONARIOS.Model.Tables;
using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;

namespace API_ARMAZENA_FUNCIONARIOS.Controllers
{
    [ApiController]
    [Route("api/setores")]
    public class SetoresController : Controller
    {
        private readonly ISetores setoresRep;

        public SetoresController(ISetores setoresRep)
        {
            this.setoresRep = setoresRep;
        }

        [HttpGet]
        [Route("/PegarSetor")]
        public async Task<IActionResult> GetSetor(string nome) 
        {
            SetoresResponse setor = await setoresRep.PegarSetor(nome);
            if (setor == null)
            {
                return BadRequest(new {Message="Setor nao existe"});
            }
            return Ok(setor);
        }


        [HttpGet]
        [Route("/listarSetores")]
        public async Task<IActionResult> GetSetores()
        {
            List<SetoresResponse> lista = await setoresRep.ListarSetores();
            if ( lista != null)
            {
                return Ok(lista);
            }

            return BadRequest(new { Message = "Não existem Setores a serem Listados" });
        }

        [HttpPost]
        [Route("/salvarSetor")]
        public async Task<IActionResult> SalvarSetor([FromBody] SetoresRequest setor) {
            ModelSetores setorModel = new ModelSetores(setor.nome, setor.qtd_funcionarios, setor.localizacao,EnumStatus.ativo);

            if(await setoresRep.SalvarSetor(setor))
            {
                return Ok(new { Message = "Salvo com sucesso" });
            }
            return BadRequest(new { Message = "Nao foi salvo" });

        }

        [HttpPut]
        [Route("/atualizarSetor")]
        public async Task<IActionResult> AtualizarSetor([FromBody] SetoresRequest setorNovo,string nomeSetor)
        {
            
            if (await setoresRep.AtualizarSetor(nomeSetor,setorNovo))
            {
                return Ok(new { Message = "Salvo com sucesso" });
            }
            return BadRequest(new { Message = "Nao foi salvo" });

        }

        [HttpDelete]
        [Route("/removerSetor")]
        public async Task<IActionResult> RemoverSetor(string nome)
        {
            if(await setoresRep.RemoverSetor(nome))
            {
                return Ok(new {Message = "Removido com sucesso"});
            }

            return BadRequest(new { Message = "Nao Removido - Nao existe " });
         }

    }
}
