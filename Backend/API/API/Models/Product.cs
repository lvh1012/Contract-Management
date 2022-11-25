using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class Product : BaseModel
    {
        [StringLength(512)]
        public string Size { get; set; }

        public int Price { get; set; }

        [StringLength(64)]
        public string Unit { get; set; }

        public ICollection<ContractProduct> ContractProducts { get; set; }
    }
}