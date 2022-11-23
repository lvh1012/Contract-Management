using API.Model;
using API.Services.Interface;

namespace API.Controllers
{
    public class ProductController : BaseController<IProductService, Product>
    {
        public ProductController(IProductService service) : base(service)
        {
        }
    }
}