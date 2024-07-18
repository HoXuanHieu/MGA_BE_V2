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

    public async Task<String> CreateMangaAsync(MangaEntity entity)
    {
        try
        {
            await _context.AddAsync(entity);
            _context.SaveChanges();
            _logger.LogInformation($"Manga has been created successful");
            return Common.Message.MESSAGE_MANGA_CREATE_SUCCESSFUL;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Server can not create new manga, with error: {ex.Message}");
            return Common.Message.MESSAGE_MANGA_CREATE_FAIL;
        }
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
            _logger.LogInformation($"Create manga successful.");
            return Common.Message.MESSAGE_MANGA_DELETE_SUCCESSFUL;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Delete manga fail with message: {ex.Message}");
            return Common.Message.MESSAGE_MANGA_DELETE_FAIL;
        }
    }

    public async Task<List<MangaEntity>> GetAllMangaAsync()
    {
        var result = await _context.Mangas.Where(x => !x.IsDelete).ToListAsync();
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
