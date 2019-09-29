using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ServicePlugin.Impl;
using TestCommon.Contracts;
using TestCommon.Domain;

namespace ServicePlugin
{
    public class ClientsPlugin : IPlugin<Client>
    {
        private readonly ILogger<ClientsPlugin> _logger;
        private readonly IRepository<Client> _repository = new InMemoryClientsRepository();
        
        public ClientsPlugin(ILogger<ClientsPlugin> logger)
        {
            _logger = logger;
            _repository.SaveAsync(new Client(1,31,123456789,"Client Test","C# Developer",0)).Wait();
        }

        public async Task<Client> GetClientAsync(int id)
        {
            _logger.LogDebug($"Invoked IPlugin<Client>.GetClientAsync with id={id}");
            return await _repository.GetAsync(id);
        }

        public async Task UpdateClientAsync(Client data)
        { 
            _logger.LogDebug($"Invoked IPlugin<Client>.UpdateClientAsync with data which has id={data.Id}");
            await _repository.SaveAsync(data);
        } 
    }
}