using Models.Entities;

namespace Repositories;

public class ChapterRepository : IChapterRepository
{
    public Task<ChapterEntity> CreateChapterAsync(ChapterEntity request)
    {
        throw new NotImplementedException();
    }

    public Task<List<ChapterEntity>> GetAllChapterAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveChapterAsync(string chapterId)
    {
        throw new NotImplementedException();
    }
}
