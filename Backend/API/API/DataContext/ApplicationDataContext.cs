using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.DataContext
{
    public class ApplicationDataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("ApplicationDatabase");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ContractProduct> ContractProducts { get; set; }
    }
}