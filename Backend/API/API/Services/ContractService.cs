using API.DTOs;
using API.Extensions;
using API.Model;
using API.Repository.Interface;
using API.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ContractService : BaseService<IContractRepository, Contract>, IContractService
    {
        private readonly ITemplateService _templateService;

        public ContractService(IContractRepository repository, ITemplateService templateService) : base(repository)
        {
            _templateService = templateService;
        }

        public override async Task<DataPage> GetData(Page page, string query = null)
        {
            var dataQuery = _repository.Get();
            if (!string.IsNullOrEmpty(query))
                dataQuery = dataQuery.Where(r => r.Name.Contains(query));

            dataQuery = dataQuery.Include(r => r.Customer);
            var data = await dataQuery.GetPage(page);
            var result = new DataPage
            {
                Data = data,
                Page = page
            };
            return result;
        }

        public async Task<string> ExportContract(Guid id)
        {
            return await _templateService.ExportContract(id);
        }
    }
}