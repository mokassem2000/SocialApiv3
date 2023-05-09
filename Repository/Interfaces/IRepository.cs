namespace SocialClint.Repository.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(string id);
        Task<bool> DeleteByIdAsync(string id);
        Task<bool> saveChaengesAsync();
        Task<IEnumerable<T>> AllAsync();
        Task<bool> UpdateAsync(T entity);
       
    }
}
