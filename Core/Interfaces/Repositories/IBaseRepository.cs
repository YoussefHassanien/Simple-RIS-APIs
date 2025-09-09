namespace Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAll(uint page = 1, uint limit = 10);
        Task<T?> GetById(uint id, string[]? includes = null);
        Task<T?> Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        
    }
}
