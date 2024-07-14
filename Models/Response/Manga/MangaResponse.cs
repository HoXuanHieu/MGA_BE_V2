namespace Models;

public class MangaResponse
{
    public string mangaId { get; set; }
    public string mangaName { get; set; }
    public String MangaImage { get; set; }
    public List<Categories> Categories { get; set; }
    public DateTime DateUpdated { get; set; }

    public MangaResponse(string mangaId, string mangaName, string mangaImage, List<Categories> categories, DateTime dateUpdated)
    {
        this.mangaId = mangaId;
        this.mangaName = mangaName;
        MangaImage = mangaImage;
        Categories = categories;
        DateUpdated = dateUpdated;
    }
}
