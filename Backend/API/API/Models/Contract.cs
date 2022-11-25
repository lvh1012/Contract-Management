namespace API.Model
{
    public class Contract : BaseModel
    {
        public int Total { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ContractProduct> ContractProducts { get; set; }
    }
}