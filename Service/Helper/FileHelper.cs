using Microsoft.AspNetCore.Http;

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
                using (FileStream stream = File.Create(rootPath + fileContent.FileName))
                {
                    await fileContent.CopyToAsync(stream);
                    stream.Flush();
                }
                return fileContent.FileName;
            }
            catch (Exception ex)
            {
                return Common.Message.MESSAGE_FILE_SAVE_FAIL + $" Error: {ex.Message}";
            }
        }
    }
}
