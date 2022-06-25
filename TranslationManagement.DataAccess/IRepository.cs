namespace TranslationManagement.DataAccess
{
    public interface IRepository<T> where T: new()
    {
        Task<T?> Get(int id);

        IEnumerable<T> GetAll();

        Task Update(int id, T data);

        Task<int> Create(T data);

        Task Delete(int id);
    }
}
