namespace Models;

public class CreateChapterRequest
{
    public String ChapterName { get; set; }
    public String MangaId { get; set; }
    public List<CreateChapterImageRequest> ChapterImage { get; set; }
}
