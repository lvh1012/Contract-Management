using API.DataContext;
using API.Model;
using API.Repository.Interface;

namespace API.Repository
{
    public class ContractProductRepository : BaseRepository<ContractProduct>, IContractProductRepository
    {
        public ContractProductRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }
    }
}