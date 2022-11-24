namespace API.Model
{
    public class Customer : BaseModel
    {
        public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
    }
}