using Models;
using Models.Entities;

namespace Repositories;

public interface IMangaRepository
{
    Task<List<MangaEntity>> GetAllMangaAsync();
    Task<String> CreateUserAsync(MangaEntity entity);
    Task<MangaEntity> GetManagByIdAsync(String mangaId);
    Task<MangaEntity> UpdateMangaAsync(MangaEntity entity);
    Task<String> DeleteMangaByIdAsync(String mangaId);
}
