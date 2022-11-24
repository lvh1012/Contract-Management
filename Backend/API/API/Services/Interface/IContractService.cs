using API.Model;

namespace API.Services.Interface
{
    public interface IContractService : IBaseService<Contract>
    {
        Task<string> ExportContract(Guid id);
    }
}