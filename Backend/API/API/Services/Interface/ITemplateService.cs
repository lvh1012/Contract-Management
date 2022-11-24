namespace API.Services.Interface
{
    public interface ITemplateService
    {
        Task<string> ExportContract(Guid id);
    }
}