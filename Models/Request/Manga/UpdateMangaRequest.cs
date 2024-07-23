using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class UpdateMangaRequest
{
    [Required]
    public string MangaId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public IFormFile MangaImage { get; set; }

    public String Description { get; set; }

    [Required]
    public List<Categories> Categories { get; set; }

    [Required]
    public String AuthorId { get; set; }

    [Required]
    public String ModifiedBy { get; set; }
}
