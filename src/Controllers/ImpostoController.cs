using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModCidadao.Models;
using ModCidadao.Repositories;

namespace ModCidadao.Controllers {

    [ApiController]
    [Route("[controller]")]    
    public class ImpostoController: ControllerBase 
    {
        private readonly IPTURepository iPTURepository;
        public ImpostoController(IPTURepository iPTURepository)
        {
            this.iPTURepository = iPTURepository;
        }

        [HttpPost("consulta")]
        public async Task<IActionResult> Consulta(ImpostoQuery consulta) {

            if (!consulta.IsValid()) {
                return BadRequest(consulta.Errors());
            }
            try
            {
                var retorno = await iPTURepository.GetByImpostoQuery(consulta);
                return Ok(retorno);                
            }
            catch (System.Exception ex)
            {
                return BadRequest($"${ex.Message} \n ${ex.StackTrace}");
            }

        } 
    }
}