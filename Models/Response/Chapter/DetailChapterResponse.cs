namespace Models;

public class DetailChapterResponse
{
    public String ChapterId { get; set; }
    public String ChapterName { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public String MangaId { get; set; }
    public String MangaName { get; set; }
    public List<ChapterImageResponse> Images { get; set; }
}
