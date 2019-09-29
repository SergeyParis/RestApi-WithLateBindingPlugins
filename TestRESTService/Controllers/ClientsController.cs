using System.Threading.Tasks;
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

        public ClientsController(ILogger<ClientsController> logger ,IPlugin<Client> clientPlugin)
        {
            _logger = logger;
            _clientPlugin = clientPlugin;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ClientDto> Get(int id)
        {
            return (await _clientPlugin.GetClientAsync(id))?.Map();
        }

        [HttpPost]
        public async Task Post([FromBody]ClientDto dto)
        {
            await _clientPlugin.UpdateClientAsync(dto.Map());
        }

        [HttpPost]
        [Route("{id}/addAge")]
        public async Task<ClientDto> AddAge(int id) => await SetAge(id, 1);
        
        [HttpPost]
        [Route("{id}/addAge/{count}")]
        public async Task<ClientDto> SetAge(int id, int count)
        {
            var client = await _clientPlugin.GetClientAsync(id);

            if (client == null)
                return null;

            client = client.SetAge(client.Age + count);
            await _clientPlugin.UpdateClientAsync(client);
            
            return (await _clientPlugin.GetClientAsync(id)).Map();
        }
    }
}