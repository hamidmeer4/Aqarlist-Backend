namespace Aqarlist.Core.Services.Service_Interface
{
    public interface IFileService
    {
        Task<int> UploadFileAsync(IFormFile file, int? propertyId = null);
    }
}
