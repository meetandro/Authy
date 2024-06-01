namespace StoreManagementMVC.Services
{
    public class FileService(IWebHostEnvironment environment) : IFileService
    {
        private readonly IWebHostEnvironment _environment = environment;

        public string SaveFileInFolder(IFormFile formFile, string folder)
        {
            string fileExtension = Path.GetExtension(formFile.FileName);
            string fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}{fileExtension}";

            string fullPath = Path.Combine(_environment.WebRootPath, folder, fileName);
            using (var stream = File.Create(fullPath))
            {
                formFile.CopyTo(stream);
            }

            return fileName;
        }

        public void DeleteFileInFolder(string fileName, string folder)
        {
            string fullPath = Path.Combine(_environment.WebRootPath, folder, fileName);
            File.Delete(fullPath);
        }
    }
}
