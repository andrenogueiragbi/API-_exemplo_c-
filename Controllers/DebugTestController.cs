using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PrimeiraAPI.Controllers
{
    [ApiController]
    [Route("teste/debugs/trinee")]
    public class DebugTestController : ControllerBase
    {
        private readonly ILogger<DebugTestController> _logger;

        public DebugTestController(ILogger<DebugTestController> logger)
        {
            _logger = logger;
        }

        [HttpDelete]
        public IActionResult get()
        {

            Random random = new Random(); // Cria uma instância de Random
            int numeroAleatorio = random.Next(0, 2); // Gera um número aleatório entre 0 e 1
            _logger.Log(LogLevel.Critical, "Isso e somente o teste valor {0}", numeroAleatorio);

            List<object> lista = new List<object> { };

            lista.Add(new { teste = 0 });
            lista.Add(new { teste = 1 });
            lista.Add(new { teste = 2 });

            if (numeroAleatorio == 0) throw new Exception("Ops isso foi um teste");

            return Ok(lista);
        }


    }
}