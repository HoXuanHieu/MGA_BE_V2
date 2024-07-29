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

    public Task<ChapterEntity> CreateChapterAsync(ChapterEntity request)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ChapterEntity>> GetAllChapterAsync(String mangagId)
    {
        var list = new List<ChapterEntity>();
        var data = await _context.Chapters.Where(x => x.MangaId == mangagId).ToListAsync();
        foreach (var item in data) {
            list.Add(item);
        }
        return list;
    }

    public Task<bool> RemoveChapterAsync(string chapterId)
    {
        throw new NotImplementedException();
    }
}
