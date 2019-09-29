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

        public ClientsController(ILogger<ClientsController> logger
            ,IPlugin<Client> clientPlugin
            )
        {
            _logger = logger;
            _clientPlugin = clientPlugin;
        }

        [HttpGet]
        public ClientDto Get([FromQuery]int id)
        {
            return _clientPlugin.GetClient(id).Map();
        }

        [HttpPost]
        public void Post([FromBody]ClientDto dto)
        {
            _clientPlugin.UpdateClient(dto.Map());
        }
    }
}