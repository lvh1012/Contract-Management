using API.DataContext;
using API.Model;
using API.Repository.Interface;

namespace API.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }
    }
}