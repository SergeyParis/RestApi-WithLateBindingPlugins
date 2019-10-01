using System.Threading.Tasks;

namespace TestCommon.Contracts
{
    public interface IPlugin<T>
    {
        Task<T> GetAsync(int id);

        Task UpdateAsync(T data);
    }
}