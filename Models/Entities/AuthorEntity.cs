using Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class AuthorEntity
{
    [Key]
    public String AuthorId { get; set; } = Guid.NewGuid().ToString();
    public String AuthorName { get; set; }  
    public String Description { get; set; }
    public String LastActivity { get; set; } = "";

    public List<MangaEntity> Mangas { get; set; }
}
