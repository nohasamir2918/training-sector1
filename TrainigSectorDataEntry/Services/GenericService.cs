using System.Linq.Expressions;
using TrainigSectorDataEntry.Interface;

namespace TrainigSectorDataEntry.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepo<T> _repository;

        public GenericService(IGenericRepo<T> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false, params Expression<Func<T, object>>[] includes) =>
            _repository.GetAllAsync(includeDeleted, includes);

        public Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes) =>
            _repository.GetByIdAsync(id, includes);

        public Task AddAsync(T entity) =>
            _repository.AddAsync(entity);

        public Task UpdateAsync(T entity) =>
            _repository.UpdateAsync(entity);

        public Task DeleteAsync(int id) =>
            _repository.DeleteAsync(id);

        public Task ActivateAsync(int id) =>
            _repository.ActivateAsync(id);

        public Task DeactivateAsync(int id) =>
            _repository.DeactivateAsync(id);
        public Task<IEnumerable<T>> GetDropdownListAsync()
        {
            
            return _repository.GetAllAsync(includeDeleted: false);
        }
   

        public async Task<IEnumerable<T>>  GetAllAsyncByEducationalFacilitiesId(bool includeDeleted, int EducationalFacilitiesId, params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetAllAsyncByEducationalFacilitiesId(includeDeleted, EducationalFacilitiesId, includes);
        }
        public async Task<List<T>> GetManyAllAsyncByEducationalFacilitiesId( bool isDeleted, int educationalFacilitiesId, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            return await _repository.GetManyAllAsyncByEducationalFacilitiesId(isDeleted, educationalFacilitiesId,null);
        }

        public Task<IEnumerable<T>> GetByFilterAsync(
          Expression<Func<T, bool>> filter,
          bool includeDeleted = false,
          params Expression<Func<T, object>>[] includes)
          => _repository.GetByFilterAsync(filter, includeDeleted, includes);
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }
    }
}

