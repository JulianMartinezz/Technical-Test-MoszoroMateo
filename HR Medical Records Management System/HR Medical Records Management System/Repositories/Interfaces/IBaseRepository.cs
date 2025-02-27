namespace HR_Medical_Records_Management_System.Repositories.Interfaces
{
    public interface IBaseRepository<T, TKey> where T  : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> GetByIdAsync(TKey id);
        Task<List<T>> GetListAsync();
    }
}
