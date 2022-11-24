using API.DTOs;

namespace API.Services.Interface
{
    public interface IBaseService<TModel>
    {
        Task<DataPage> GetData(Page page, string query = null);

        Task<TModel> GetById(Guid id);

        Task<TModel> Insert(TModel model);

        Task<TModel> Update(Guid id, TModel model);

        Task<bool> Delete(Guid id);

        Task<bool> DeleteMultiple(ListID listID);

        Task<List<TModel>> GetMultiple(ListID listID);
    }
}