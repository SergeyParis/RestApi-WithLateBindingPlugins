using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestCommon.Contracts;
using TestCommon.Domain;
using TestRESTService.Contracts;
using TestRESTService.Mappers;

namespace TestRESTService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IPlugin<Client> _clientPlugin;

        public ClientsController(ILogger<ClientsController> logger, IPlugin<Client> clientPlugin)
        {
            _logger = logger;
            _clientPlugin = clientPlugin;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogDebug("Start handle request [clients/{id} | get]");
            var result = (await _clientPlugin.GetAsync(id))?.Map();

            if (result == null)
            {
                _logger.LogDebug($"client with id={id} doesn't exist [clients/{{id}} | get]");
                return BadRequest();
            }
            
            _logger.LogDebug("Finish handle request with OK status [clients/{id} | get]");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ClientDto dto)
        {
            _logger.LogDebug("Start handle request [clients/ | post]");
            await _clientPlugin.UpdateAsync(dto.Map());
            
            _logger.LogDebug("Finish handle request with OK status [clients/ | post]");
            return Ok();
        }

        [HttpPost]
        [Route("{id}/addAge")]
        public async Task<IActionResult> AddAge(int id) => await AddAge(id, 1);
        
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("{id}/addAge/{count}")]
        public async Task<IActionResult> AddAge(int id, int count)
        {
            _logger.LogDebug($"Start handle request [clients/{{id}}/addAge/ | post]");
            var client = await _clientPlugin.GetAsync(id);

            if (client == null)
            {
                _logger.LogDebug($"client with id={id} doesn't exist [clients/{{id}}/addAge/ | post]");
                return BadRequest();
            }

            await _clientPlugin.UpdateAsync(client.SetAge(client.Age + count));
            
            _logger.LogDebug($"Finish handle request with OK status [clients/{{id}}/addAge/ | post]");
            return Ok(client.Map());
        }
    }
}