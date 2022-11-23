namespace API.Model
{
    public class Contract : BaseModel
    {
        public int Total { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<ContractProduct> ContractProducts { get; set; } = new List<ContractProduct>();
    }
}