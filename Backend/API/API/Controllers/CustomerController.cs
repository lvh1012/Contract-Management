using API.Model;
using API.Services.Interface;

namespace API.Controllers
{
    public class CustomerController : BaseController<ICustomerService, Customer>
    {
        public CustomerController(ICustomerService service) : base(service)
        {
        }
    }
}