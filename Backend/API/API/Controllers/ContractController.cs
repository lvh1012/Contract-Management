using API.Model;
using API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ContractController : BaseController<IContractService, Contract>
    {
        public ContractController(IContractService service) : base(service)
        {
        }

        [HttpGet("{id}/Export")]
        public async Task<IActionResult> ExportContract(Guid id)
        {
            //var path = await _service.ExportContract(id);
            var path = @"C:\Users\EthanLe\Downloads\IMG20220515172459.jpg";
            using Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            string mimeType = "application/octet-stream";
            return new FileStreamResult(stream, mimeType)
            {
                FileDownloadName = "test"
            };
        }
    }
}