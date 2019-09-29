using TestCommon.Domain;

namespace TestCommon.Contracts
{
    public interface IPlugin<T>
    {
        T GetClient(int id);

        void UpdateClient(T data);
    }
}