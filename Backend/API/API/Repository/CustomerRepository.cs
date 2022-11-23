using API.DataContext;
using API.Model;
using API.Repository.Interface;

namespace API.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }
    }
}