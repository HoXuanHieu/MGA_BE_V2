using Models;

namespace Repositories;
public interface IChapterImageRepository
{
    Task<List<ChapterImageEntity>> GetImageByChapterIdAsync(string chapterId);
    Task<String> DeleteImageByChapterId(string chapterId);
    Task<List<ChapterImageEntity>> CreateImageAsync(List<ChapterImageEntity> dataList);
}

