namespace API.Model
{
    public class Customer : BaseModel
    {
        public List<Contract> Contracts { get; set; } = new List<Contract>();
    }
}