using Models.Entities;

namespace Models;

public class AuthorEntity
{
    public String AuthorId { get; set; } = Guid.NewGuid().ToString();
    public String AuthorName { get; set; }  
    public String Description { get; set; }

    public List<MangaEntity> Mangas { get; set; }
}
