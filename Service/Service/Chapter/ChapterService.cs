using Common;
using Microsoft.Extensions.Logging;
using Models;
using Repositories;
using Web_API.ResponseModel;

namespace Service;

public class ChapterService : IChapterService
{
    private readonly IChapterImageService _chapterImageService;
    private readonly IChapterRepository _chapterRepository;
    private readonly ILogger<ChapterService> _logger;
    private readonly IMangaService _mangaService;

    public ChapterService(IChapterRepository chapterRepository, IChapterImageService chapterImageService, ILogger<ChapterService> logger, IMangaService mangaService)
    {
        _chapterRepository = chapterRepository;
        _logger = logger;
        _chapterImageService = chapterImageService;
        _mangaService = mangaService;
    }
    public Task<ApiResponse<ChapterResponse>> CreateChapterAsync(CreateChapterImageRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<List<ChapterResponse>>> GetAllChapterByMangaIdAsync(string mangaId)
    {
        var response = new List<ChapterResponse>();
        var data = await _chapterRepository.GetAllChapterAsync(mangaId);
        if (!data.Any())
        {
            return new ApiResponse<List<ChapterResponse>>(Message.MESSAGE_CHAPTER_NO_DATA, response, 203);
        }
        foreach(var item in data)
        {
            response.Add(new ChapterResponse
            {
                ChapterId = item.ChapterId,
                ChapterName = item.ChapterName,
                DateCreate = item.DateCreated
            });
        }
        return new ApiResponse<List<ChapterResponse>>(Message.MESSAGE_CHAPTER_GET_SUCCESSFUL, response, 200);
    }

    public async Task<ApiResponse<DetailChapterResponse>> GetChapterDetailAsync(string chapterId)
    {
        var data =  await _chapterRepository.GetChapterByIdAsync(chapterId);
        if(data == null)
            return new ApiResponse<DetailChapterResponse>(Message.MESSAGE_CHAPTER_DOES_NOT_EXIST, null, 404);
        var manga = await _mangaService.GetMangaByIdAsync(data.MangaId);
        var response = new DetailChapterResponse() { 
            ChapterId = data.ChapterId,
            ChapterName = data.ChapterName,
            MangaId = data.MangaId,
            DateCreated = data.DateCreated,
            MangaName = manga.Content.MangaName,
            Images = new List<ChapterImageResponse>()
        };
        var chapterImage = await _chapterImageService.GetChapterImagesByChapterIdAsync(chapterId);
        response.Images = chapterImage;
        return new ApiResponse<DetailChapterResponse>(Message.MESSAGE_CHAPTER_GET_SUCCESSFUL, response, 200);
    }

    public Task<ApiResponse<bool>> RemoveChapterAsync(string chapterId)
    {
        throw new NotImplementedException();
    }
}
