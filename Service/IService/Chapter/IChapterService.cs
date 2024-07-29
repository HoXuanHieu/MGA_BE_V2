using Models;
using Web_API.ResponseModel;

namespace Service;

public interface IChapterService
{
    Task<ApiResponse<List<ChapterResponse>>> GetAllChapterByMangaIdAsync(String mangaId);
    Task<ApiResponse<ChapterResponse>> CreateChapterAsync(CreateChapterImageRequest request);
    Task<ApiResponse<bool>> RemoveChapterAsync(String chapterId);
    Task<ApiResponse<DetailChapterResponse>> GetChapterDetailAsync(String chapterId);
}
