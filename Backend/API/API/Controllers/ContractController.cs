using API.Model;
using API.Services.Interface;

namespace API.Controllers
{
    public class ContractController : BaseController<IContractService, Contract>
    {
        public ContractController(IContractService service) : base(service)
        {
        }
    }
}