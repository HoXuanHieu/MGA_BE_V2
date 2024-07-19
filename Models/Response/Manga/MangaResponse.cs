namespace Models;

public class MangaResponse
{
    public String MangaId { get; set; }
    public String MangaName { get; set; }
    public String MangaImage { get; set; }
    public List<Categories> Categories { get; set; }
    public String AuthorId { get; set; }
    public String AuthorName { get; set; }

    public DateTime DateUpdated { get; set; }

    public MangaResponse(string mangaId, string mangaName, string mangaImage, List<Categories> categories,String authorId, String authorName, DateTime dateUpdated)
    {
        this.MangaId = mangaId;
        this.MangaName = mangaName;
        MangaImage = mangaImage;
        Categories = categories;
        DateUpdated = dateUpdated;
        AuthorId = authorId;
        AuthorName = authorName;
    }
}
