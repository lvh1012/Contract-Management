using API.DTOs;
using API.Exceptions;
using API.Extensions;
using API.Model;
using API.Repository.Interface;
using API.Services.Interface;

namespace API.Services
{
    public class BaseService<TRepository, TModel> : IBaseService<TModel>
        where TRepository : IBaseRepository<TModel>
        where TModel : BaseModel
    {
        protected readonly IBaseRepository<TModel> _repository;

        public BaseService(IBaseRepository<TModel> repository)
        {
            _repository = repository;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            var exist = await _repository.Exists(id);
            if (!exist)
            {
                throw new HttpResponseException(StatusCodes.Status400BadRequest, "ID is invalid");
            }

            return await _repository.Delete(id);
        }

        public virtual async Task<DataPage> GetData(Page page, string query = null)
        {
            var dataQuery = _repository.Get();
            if (!string.IsNullOrEmpty(query))
                dataQuery = dataQuery.Where(r => r.Name.Contains(query));

            var data = await dataQuery.GetPage(page);
            var result = new DataPage
            {
                Data = data,
                Page = page
            };
            return result;
        }

        public virtual async Task<TModel> GetById(Guid id)
        {
            var result = await _repository.GetById(id);
            if (result is null)
            {
                throw new HttpResponseException(StatusCodes.Status400BadRequest, "ID is invalid");
            }

            return result;
        }

        public virtual async Task<TModel> Insert(TModel model)
        {
            return await _repository.Insert(model);
        }

        public virtual async Task<TModel> Update(Guid id, TModel model)
        {
            var exist = await _repository.Exists(id);
            if (!exist)
            {
                throw new HttpResponseException(StatusCodes.Status400BadRequest, "ID is invalid");
            }

            return await _repository.Update(model);
        }

        public virtual async Task<bool> DeleteMultiple(ListID listID)
        {
            return await _repository.DeleteMultiple(listID.Ids);
        }

        public virtual async Task<List<TModel>> GetMultiple(ListID listID)
        {
            return await _repository.GetMultiple(listID.Ids);
        }
    }
}