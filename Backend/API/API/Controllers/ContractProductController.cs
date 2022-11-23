using API.Model;
using API.Services.Interface;

namespace API.Controllers
{
    public class ContractProductController : BaseController<IContractProductService, ContractProduct>
    {
        public ContractProductController(IContractProductService service) : base(service)
        {
        }
    }
}