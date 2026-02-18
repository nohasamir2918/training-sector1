using System.Linq.Expressions;

namespace TrainigSectorDataEntry.Interface
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> GetByFilterAsync(
                Expression<Func<T, bool>> filter,
                bool includeDeleted = false,
                params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsyncByEducationalFacilitiesId(bool includeDeleted = false, int EducationalFacilitiesId=0, params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task ActivateAsync(int id);
        Task DeactivateAsync(int id);
        Task<IEnumerable<T>> GetDropdownListAsync();
        Task<List<T>> GetManyAllAsyncByEducationalFacilitiesId(
    bool isDeleted,
    int educationalFacilitiesId,
    Func<IQueryable<T>, IQueryable<T>> include = null

);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

    }
}
