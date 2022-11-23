using API.Model;
using API.Repository.Interface;
using API.Services.Interface;

namespace API.Services
{
    public class CustomerService : BaseService<ICustomerRepository, Customer>, ICustomerService
    {
        public CustomerService(ICustomerRepository repository) : base(repository)
        {
        }
    }
}