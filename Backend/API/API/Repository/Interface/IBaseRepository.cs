using System.Linq.Expressions;

namespace API.Repository.Interface
{
    public interface IBaseRepository<TModel>
    {
        Task<TModel> GetById(Guid id);

        IQueryable<TModel> Get(Expression<Func<TModel, bool>> expression = null);

        Task<TModel> Insert(TModel entity);

        Task<TModel> Update(TModel entity);

        Task<bool> Delete(TModel entity);

        Task<bool> Delete(Guid id);

        Task InsertMultiple(List<TModel> entities);

        Task UpdateMultiple(List<TModel> entities);

        Task<bool> DeleteMultiple(List<TModel> entities);

        Task<bool> DeleteMultiple(List<Guid> listId);
        Task<List<TModel>> GetMultiple(List<Guid> listId);
        Task<bool> Exists(Guid id);
    }
}