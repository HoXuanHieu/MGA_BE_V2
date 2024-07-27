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

    public async Task<string> DeleteChapterImagesByChapterIdAsync(string chapterId)
    {
        var result= await _chapterImageRepository.DeleteImageByChapterId(chapterId);
        if (result != Common.Message.MESSAGE_CHAPTER_IMAGE_DELETE_SUCCESSFUL)
        {
            throw new Exception(result);
        }
        else
        {
            return result;
        }
    }

    public async Task<List<ChapterImageResponse>> GetChapterImagesByChapterIdAsync(string chapterId)
    {
        var result = new List<ChapterImageResponse>();
        var data = await _chapterImageRepository.GetImageByChapterIdAsync(chapterId);
        foreach(var item in data)
        {
            result.Add(new ChapterImageResponse
            {
                ChapterImageId = item.ChapterImageId,
                ChapterImageName = item.ChapterImageName,
                ChapterImagePath = item.ChapterImagePath,
            });
        }
        return result;
    }
}

