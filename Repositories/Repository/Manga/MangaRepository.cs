using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Entities;

namespace Repositories;

public class MangaRepository : IMangaRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<MangaRepository> _logger;
    public MangaRepository(DatabaseContext context, ILogger<MangaRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<string> CreateUserAsync(MangaEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<String> DeleteMangaByIdAsync(string mangaId)
    {
        var result = await GetManagByIdAsync(mangaId);
        if (result == null)
            return Common.Message.MESSAGE_MANGA_DOES_NOT_EXIST;
        else if (result.IsDelete) return Common.Message.MESSAGE_MANGA_ALREADY_DELETE;
        try
        {
            result.IsDelete = true;
            _context.Update(result);
            await _context.SaveChangesAsync();
            return Common.Message.MESSAGE_MANGA_DELETE_SUCCESSFUL;
        }
        catch(Exception ex)
        {
            _logger.LogError($"Delete manga fail with message: {ex.Message}");
            return Common.Message.MESSAGE_MANGA_DELETE_FAIL;
        }
    }

    public async Task<List<MangaEntity>> GetAllMangaAsync()
    {
        var result = await _context.Mangas.Where(x => x.IsApproval && !x.IsDelete).ToListAsync();
        return result;
    }

    public async Task<MangaEntity> GetManagByIdAsync(string mangaId)
    {
        var result = await _context.Mangas.FirstOrDefaultAsync(x => x.MangaId == mangaId && x.IsApproval && !x.IsDelete);
        return result;
    }

    public async Task<MangaEntity> UpdateMangaAsync(MangaEntity entity)
    {
        var result = _context.Mangas.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
}
