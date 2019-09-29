namespace TestCommon.Contracts
{
    public interface IRepository<T>
    {
        void Save(T data);

        T Get(int id);
    }
}