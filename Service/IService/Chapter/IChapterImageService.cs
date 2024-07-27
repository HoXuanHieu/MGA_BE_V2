using Models;

namespace Service;

public interface IChapterImageService
{
    Task<List<ChapterImageResponse>> GetChapterImagesByChapterIdAsync(string chapterId);
    Task<List<ChapterImageResponse>> CreateChapterImagesAsync(String chapterId, String chapterName, String mangaName, List<CreateChapterImageRequest> chapterImages);
    Task<String> DeleteChapterImagesByChapterIdAsync(string chapterId);
}
