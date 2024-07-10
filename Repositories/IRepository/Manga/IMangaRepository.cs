using Models;
using Models.Entities;

namespace Repositories;

public interface IMangaRepository
{
    Task<List<MangaEntity>> getallManga();
    Task<MangaEntity> GetManagById(String mangaId);
    Task<MangaEntity> UpdateManga(MangaEntity entity);
    Task<bool> DeleteMangaById(String mangaId);
}
