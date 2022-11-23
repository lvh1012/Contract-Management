using API.Model;
using API.Repository.Interface;
using API.Services.Interface;

namespace API.Services
{
    public class ContractProductService : BaseService<IContractProductRepository, ContractProduct>, IContractProductService
    {
        public ContractProductService(IContractProductRepository repository) : base(repository)
        {
        }
    }
}