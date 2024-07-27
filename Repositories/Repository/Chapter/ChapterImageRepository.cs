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

    public Task<List<ChapterImageEntity>> CreateImageAsync(List<ChapterImageEntity> dataList)
    {
        throw new NotImplementedException();
    }

    public async Task<String> DeleteImageByChapterId(string chapterId)
    {
        var data = await _context.ChapterImages.Where(x => x.ChapterId == chapterId).ToListAsync();
        if(data == null)
        {
            return "";
        }
        else {
            try
            {
                _context.ChapterImages.RemoveRange(data);
                await _context.SaveChangesAsync();
                return "";
            }
            catch(Exception ex)
            {
                _logger.LogError($"Delete Chapter image fail, throw with exception: {ex.Message}");
                return "";
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
