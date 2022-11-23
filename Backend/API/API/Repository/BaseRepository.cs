using API.DataContext;
using API.Model;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repository
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : BaseModel
    {
        protected readonly ApplicationDataContext _context;
        protected readonly DbSet<TModel> _dbSet;

        protected BaseRepository(ApplicationDataContext applicationDataContext)
        {
            _context = applicationDataContext;
            _dbSet = _context.Set<TModel>();
        }

        public virtual async Task<TModel> Insert(TModel entity)
        {
            entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            var now = DateTime.Now;
            entity.CreatedOn = entity.CreatedOn == DateTime.MinValue ? now : entity.CreatedOn;
            entity.CreatedBy = Guid.Empty;
            entity.UpdatedOn = entity.UpdatedOn == DateTime.MinValue ? now : entity.UpdatedOn;
            entity.UpdatedBy = Guid.Empty;
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task InsertMultiple(List<TModel> entities)
        {
            var now = DateTime.Now;
            foreach (var item in entities)
            {
                item.Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
                item.CreatedOn = item.CreatedOn == DateTime.MinValue ? now : item.CreatedOn;
                item.CreatedBy = Guid.Empty;
                item.UpdatedOn = item.UpdatedOn == DateTime.MinValue ? now : item.UpdatedOn;
                item.UpdatedBy = Guid.Empty;
            }
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> Delete(TModel entity)
        {
            entity.IsDeleted = true;
            await Update(entity);
            return true;
        }

        public virtual async Task<bool> DeleteMultiple(List<TModel> entities)
        {
            foreach (var item in entities)
            {
                item.IsDeleted = true;
            }
            await UpdateMultiple(entities);
            return true;
        }

        public virtual IQueryable<TModel> Get(Expression<Func<TModel, bool>> expression = null)
        {
            if (expression == null)
            {
                return _dbSet.Where(r => !r.IsDeleted).AsNoTracking();
            }
            return _dbSet.Where(r => !r.IsDeleted).Where(expression).AsNoTracking();
        }

        public virtual async Task<TModel> GetById(Guid id)
        {
            return await Get(r => r.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<TModel> Update(TModel entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            entity.UpdatedOn = entity.UpdatedOn == DateTime.MinValue ? DateTime.Now : entity.UpdatedOn;
            entity.UpdatedBy = Guid.Empty;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateMultiple(List<TModel> entities)
        {
            _dbSet.AttachRange(entities);
            var now = DateTime.Now;
            foreach (var item in entities)
            {
                item.UpdatedOn = item.UpdatedOn == DateTime.MinValue ? now : item.UpdatedOn;
                item.UpdatedBy = Guid.Empty;
                _context.Entry(item).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            var entity = await GetById(id);
            return await Delete(entity);
        }

        public virtual async Task<bool> DeleteMultiple(List<Guid> listId)
        {
            var entities = await Get(e => listId.Contains(e.Id)).ToListAsync();
            return await DeleteMultiple(entities);
        }

        public virtual async Task<bool> Exists(Guid id)
        {
            var entity = await GetById(id);
            return entity is not null;
        }
    }
}