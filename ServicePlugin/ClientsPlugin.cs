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
            _repository.Save(new Client(1,31,123456789,"Client Test","C# Developer",0));
        }
        
        public Client GetClient(int id) => _repository.Get(id);
        
        public void UpdateClient(Client data) => _repository.Save(data);
    }
}