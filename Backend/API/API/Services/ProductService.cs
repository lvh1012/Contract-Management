using API.Model;
using API.Repository.Interface;
using API.Services.Interface;

namespace API.Services
{
    public class ProductService : BaseService<IProductRepository, Product>, IProductService
    {
        public ProductService(IProductRepository repository) : base(repository)
        {
        }
    }
}