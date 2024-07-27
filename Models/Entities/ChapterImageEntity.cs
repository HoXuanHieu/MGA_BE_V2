using Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class ChapterImageEntity
{
    [Key]
    public String ChapterImageId { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public String ChapterImagePath { get; set; } = "Image Die path";

    [Required]
    [MaxLength(70)]
    public String ChapterName { get; set; }

    [Required]
    public DateTime DateCreate { get; set; } = DateTime.Now;

    public String LastActivity { get; set; } = "";

    [Required]
    public String ChapterId { get; set; }

    public ChapterEntity Chapter { get; set; }
}
