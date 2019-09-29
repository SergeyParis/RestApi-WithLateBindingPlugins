using System.Threading.Tasks;
using ServicePlugin.Impl;
using TestCommon.Contracts;
using TestCommon.Domain;

namespace ServicePlugin
{
    public class ClientsPlugin : IPlugin<Client>
    {
        private readonly IRepository<Client> _repository = new InMemoryClientsRepository();
        
        public ClientsPlugin()
        {
            _repository.SaveAsync(new Client(1,31,123456789,"Client Test","C# Developer",0)).Wait();
        }
        
        public async Task<Client> GetClientAsync(int id) => await _repository.GetAsync(id);
        
        public async Task UpdateClientAsync(Client data) => await _repository.SaveAsync(data);
    }
}