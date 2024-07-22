using Microsoft.AspNetCore.Http;

namespace Models;

public class UpdateMangaRequest
{
    public string MangaId { get; set; }

    public string Title { get; set; }

    public IFormFile MangaImage { get; set; }

    public String Description { get; set; }

    public List<Categories> Categories { get; set; }

    public String AuthorId { get; set; }

    public String PostedBy { get; set; }
}
