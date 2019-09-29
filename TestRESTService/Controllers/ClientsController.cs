using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestRESTService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {
            
        }
    }
}