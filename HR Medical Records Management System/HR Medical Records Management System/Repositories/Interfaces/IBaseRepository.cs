namespace HR_Medical_Records_Management_System.Repositories.Interfaces
{
    public interface IBaseRepository<T, TKey,Dto> where T  : class where Dto : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(Dto dto);
        Task<T> GetByIdAsync(TKey id);
        Task<List<T>> GetListAsync();
    }
}
