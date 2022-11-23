using API.DataContext;
using API.Model;
using API.Repository.Interface;

namespace API.Repository
{
    public class ContractRepository : BaseRepository<Contract>, IContractRepository
    {
        public ContractRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }
    }
}