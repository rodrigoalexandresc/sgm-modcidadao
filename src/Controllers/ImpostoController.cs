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

        [HttpGet("testedb")]
        public async Task<IActionResult> TesteDb() 
        {
            var db = Environment.GetEnvironmentVariable("DB_USER") +  ';' + 
            Environment.GetEnvironmentVariable("DB_PASS") + ";" +
             Environment.GetEnvironmentVariable("DB_NAME") + ";" +
             Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME");

            return Ok(db);
        }

        [HttpPost("consulta")]
        public async Task<IActionResult> Consulta(ImpostoQuery consulta) {

            if (!consulta.IsValid()) {
                return BadRequest(consulta.Errors());
            }

            var retorno = await iPTURepository.GetByImpostoQuery(consulta);
            return Ok(retorno);                

            // try
            // {

            // }
            // catch (System.Exception ex)
            // {
            //     return BadRequest($"${ex.Message} \n ${ex.StackTrace}");
            // }

        } 
    }
}