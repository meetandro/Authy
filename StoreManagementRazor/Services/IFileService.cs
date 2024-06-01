namespace StoreManagementRazor.Services
{
    public interface IFileService
    {
        string SaveFileInFolder(IFormFile formFile, string folder);

        void DeleteFileInFolder(string fileName, string folder);
    }
}
