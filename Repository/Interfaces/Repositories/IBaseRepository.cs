namespace Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetById(uint id, string[]? includes = null);
        Task<T?> Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        
    }
}
