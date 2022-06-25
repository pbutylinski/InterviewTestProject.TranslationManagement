namespace TranslationManagement.DataAccess
{
    public interface IRepository<T> where T: new()
    {
        T? Get(int id);

        T? FindByName(string name);

        IEnumerable<T> GetAll();

        Task Update(T data);

        Task<int> Create(T data);

        Task Delete(int id);
    }
}
