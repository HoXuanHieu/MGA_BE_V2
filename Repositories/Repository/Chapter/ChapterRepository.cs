using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Entities;

namespace Repositories;

public class ChapterRepository : IChapterRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<ChapterRepository> _logger;
    public ChapterRepository(DatabaseContext context, ILogger<ChapterRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ChapterEntity> CreateChapterAsync(ChapterEntity request)
    {
        try
        {
            var result = await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Create Chapter successful at time: {DateTime.Now}");
            return result.Entity;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Create Chapter fail with exeption: {ex.Message}");
            throw new Exception(Message.MESSAGE_CHAPTER_CREATE_FAIL);
        }
    }

    public async Task<List<ChapterEntity>> GetAllChapterAsync(String mangagId)
    {
        var list = new List<ChapterEntity>();
        var data = await _context.Chapters.Where(x => x.MangaId == mangagId).ToListAsync();
        foreach (var item in data)
        {
            list.Add(item);
        }
        return list;
    }

    public async Task<ChapterEntity> GetChapterByIdAsync(string ChapterId)
    {
        return await _context.Chapters.FirstOrDefaultAsync(x => x.ChapterId.Equals(ChapterId));
    }

    public async Task<String> RemoveChapterAsync(string chapterId)
    {
        var entity = await GetChapterByIdAsync(chapterId);
        if (entity == null)
        {
            //throw could not found chapter
            throw new Exception(Message.MESSAGE_CHAPTER_DOES_NOT_EXIST);
        }
        try
        {
            var result = _context.Chapters.Remove(entity);
            await _context.SaveChangesAsync();
            return Message.MESSAGE_CHAPTER_DELETE_SUCCESSFUL;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Delete Chapter fail, with exception: {ex.Message}");
            throw new Exception(Message.MESSAGE_CHAPTER_DELETE_FAIL);
        }
    }
}
