using System.Threading.Tasks;

namespace TestCommon.Contracts
{
    public interface IPlugin<T>
    {
        Task<T> GetClientAsync(int id);

        Task UpdateClientAsync(T data);
    }
}