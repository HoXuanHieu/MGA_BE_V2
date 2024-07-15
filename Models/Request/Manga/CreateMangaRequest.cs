using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class CreateMangaRequest
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public IFormFile MangaImage { get; set; }

    [Required]
    [StringLength(500)]
    public String Description { get; set; }

    [Required]
    public List<Categories> Categories { get; set; }

    [Required]
    public String PostedBy { get; set; }
}
