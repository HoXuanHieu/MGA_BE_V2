using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Service.Helper
{
    public static class FileHelper
    {

        //remember to check file extension before save in local directory
        public static async Task<string> SaveImageAsync(string rootPath, IFormFile fileContent)
        {
            try
            {
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                var fileDirect = rootPath + "/" + fileContent.FileName;
                using (FileStream stream = File.Create(fileDirect))
                {
                    await fileContent.CopyToAsync(stream);
                    stream.Flush();
                }
                return fileDirect;
            }
            catch (Exception ex)
            {
                return Common.Message.MESSAGE_FILE_SAVE_FAIL + $" Error: {ex.Message}";
            }
        }

        public static bool CheckValidFileExtension(List<String> validExtensions, String fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (extension == null)
            {
                return false;
            }
            if (validExtensions.Contains(extension))
            {
                return true;
            }
            return false;
        }
    }
}
