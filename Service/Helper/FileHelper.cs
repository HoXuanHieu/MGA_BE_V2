namespace Service.Helper
{
    public static class FileHelper
    {
        public static async Task<string> SaveImageAsync(string base64, string folderName, string fileName)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, fileName);
                await File.WriteAllBytesAsync(path, bytes);
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
