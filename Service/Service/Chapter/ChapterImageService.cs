using Microsoft.Extensions.Logging;
using Models;
using Repositories;
using Service.Helper;

namespace Service;
public class ChapterImageService : IChapterImageService
{
    private readonly IChapterImageRepository _chapterImageRepository;
    private readonly ILogger<ChapterImageService> _logger;

    public ChapterImageService(IChapterImageRepository chapterImageRepository, ILogger<ChapterImageService> logger)
    {
        _chapterImageRepository = chapterImageRepository;
        _logger = logger;
    }

    public async Task<List<ChapterImageResponse>> CreateChapterImagesAsync(String chapterId, String chapterName, String mangaName, List<CreateChapterImageRequest> chapterImages)
    {
        var storePath = Common.Path.LOCAL_CHAPTER_IMAGE_STORAGE_PATH + "\\" + mangaName + "\\" + chapterName + "\\";
        var result = new List<ChapterImageResponse>();    
        var request = new List<ChapterImageEntity>();
        foreach(var item in chapterImages)
        {
            try
            {
                string imagePath = await FileHelper.SaveImageAsync(storePath, item.ChapterImage);
                var entity = new ChapterImageEntity
                {
                    ChapterId = chapterId,
                    ChapterImagePath = imagePath,
                    ChapterImageName = item.ChapterImage.FileName,
                    LastActivity = "Create image chapter",
                    DateCreate = DateTime.Now,
                };
                request.Add(entity);
            }catch (Exception ex)
            {
                throw new Exception($"Can not save image ${item.ChapterImageName} .{ex.Message}");
            }
        }
        try
        {
            var createdImages = await _chapterImageRepository.CreateImageAsync(request);
            foreach(var item in createdImages)
            {
                result.Add(new ChapterImageResponse
                {
                    ChapterImageId = item.ChapterImageId,
                    ChapterImageName = item.ChapterImageName,
                    ChapterImagePath = item.ChapterImagePath,
                });
            }
        }
        catch(Exception ex)
        {
           throw new Exception($"{ex.Message}");
        }
        return result;
    }

    public Task<string> DeleteChapterImagesByChapterIdAsync(string chapterId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ChapterImageResponse>> GetChapterImagesByChapterIdAsync(string chapterId)
    {
        throw new NotImplementedException();
    }
}

