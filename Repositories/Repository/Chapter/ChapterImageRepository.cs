using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Repositories;

public class ChapterImageRepository : IChapterImageRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<ChapterImageRepository> _logger;

    public ChapterImageRepository(DatabaseContext context, ILogger<ChapterImageRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ChapterImageEntity>> CreateImageAsync(List<ChapterImageEntity> dataList)
    {
        var result = new List<ChapterImageEntity>();
        try
        {
            foreach (var item in dataList)
            {
                var createdItem = await _context.ChapterImages.AddAsync(item);
                result.Add(createdItem.Entity);
            }
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            var message = $"Create Chapter image fail, throw with exception: {ex.Message}";
            _logger.LogError(message);
           throw new Exception(Message.MESSAGE_CHAPTER_CREATE_SUCCESSFUL);
        }


    }

    public async Task<String> DeleteImageByChapterId(string chapterId)
    {
        var data = await _context.ChapterImages.Where(x => x.ChapterId == chapterId).ToListAsync();
        if(data == null)
        {
            return Message.MESSAGE_CHAPTER_DOES_NOT_HAVE_IMAGES;
        }
        else {
            try
            {
                _context.ChapterImages.RemoveRange(data);
                await _context.SaveChangesAsync();
                return Message.MESSAGE_CHAPTER_IMAGE_DELETE_SUCCESSFUL;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Delete Chapter image fail, throw with exception: {ex.Message}");
                return Message.MESSAGE_CHAPTER_IMAGE_DELETE_FAIL;
            }
       
        }
    }

    public async Task<List<ChapterImageEntity>> GetImageByChapterIdAsync(string chapterId)
    {
        var result = new List<ChapterImageEntity>();
        var data = await _context.ChapterImages.Where(x => x.ChapterId == chapterId).ToListAsync();
        result.AddRange(data);
        return result;
    }
}
