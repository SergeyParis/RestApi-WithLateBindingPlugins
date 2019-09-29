using System.Threading.Tasks;

namespace TestCommon.Contracts
{
    public interface IRepository<T>
    {
        Task SaveAsync(T data);

        Task<T> GetAsync(int id);
    }
}