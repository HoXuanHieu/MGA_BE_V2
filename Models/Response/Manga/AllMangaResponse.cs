namespace Models;

public class AllMangaResponse
{
    public List<MangaResponse> mangaHasApproval { get; set; }
    public List<MangaResponse> mangaNoApproval { get; set; }
}
