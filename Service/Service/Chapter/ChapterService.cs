using Models;
using Web_API.ResponseModel;

namespace Service;

public class ChapterService : IChapterService
{
    public Task<ApiResponse<ChapterResponse>> CreateChapterAsync(CreateChapterImageRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<List<ChapterResponse>>> GetAllChapterByMangaIdAsync(string mangaId)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<DetailChapterResponse>> GetChapterDetailAsync(string chapterId)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<bool>> RemoveChapterAsync(string chapterId)
    {
        throw new NotImplementedException();
    }
}
