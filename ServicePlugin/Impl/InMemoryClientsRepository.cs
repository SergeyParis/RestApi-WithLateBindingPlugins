using System.Collections.Generic;
using System.Threading.Tasks;
using TestCommon.Contracts;
using TestCommon.Domain;

namespace ServicePlugin.Impl
{
    internal class InMemoryClientsRepository : IRepository<Client>
    {
        private readonly Dictionary<int, Client> _repo = new Dictionary<int, Client>(30);
        
        public void Save(Client data)
        {
            _repo.TryGetValue(data.Id, out var current);

            if (current != null)
                _repo[data.Id] = data;
            else
                _repo.Add(data.Id, data);
        }

        public Client Get(int id)
        {
            _repo.TryGetValue(id, out var data);
            return data;
        }

        public async Task<Client> GetAsync(int id) => await Task.Factory.StartNew(() => Get(id));
        
        public async Task SaveAsync(Client data) => await Task.Factory.StartNew(() => Save(data));
    }
}