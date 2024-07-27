using Microsoft.AspNetCore.Http;

namespace Models;

public class CreateChapterImageRequest
{
    public String ChapterImageName { get; set; }
    public IFormFile ChapterImage { get; set; }
}
