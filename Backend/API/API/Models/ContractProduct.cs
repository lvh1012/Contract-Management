namespace API.Model
{
    public class ContractProduct : BaseModel
    {
        public int Quantity { get; set; }
        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}