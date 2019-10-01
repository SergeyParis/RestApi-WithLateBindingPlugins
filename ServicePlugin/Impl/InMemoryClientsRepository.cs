using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestCommon.Contracts;
using TestCommon.Domain;

namespace ServicePlugin.Impl
{
    internal class InMemoryClientsRepository : IRepository<Client>
    {
        private readonly Dictionary<int, Client> _repo = new Dictionary<int, Client>(30);
        private readonly Hashtable _locks = new Hashtable(30);

        public void Save(Client data)
        {
            if (!_locks.ContainsKey(data.Id))
                _locks.Add(data.Id, new object());

            lock (_locks[data.Id])
            {
                _repo.TryGetValue(data.Id, out var current);

                if (current != null)
                    _repo[data.Id] = data;
                else
                    _repo.Add(data.Id, data);
            }
            
            _locks.Remove(data.Id);
        }

        public Client Get(int id)
        {
            if (!_locks.ContainsKey(id))
                _locks.Add(id, new object());

            Client result = null;
            lock (_locks[id])
            {
                _repo.TryGetValue(id, out result);
            }

            _locks.Remove(id);
            return result;
        }

        public async Task<Client> GetAsync(int id) => await Task.Factory.StartNew(() => Get(id));

        public async Task SaveAsync(Client data) => await Task.Factory.StartNew(() => Save(data));
    }
}