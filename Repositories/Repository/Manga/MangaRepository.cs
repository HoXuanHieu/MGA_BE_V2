using Models.Entities;

namespace Repositories;

public class MangaRepository : IMangaRepository
{
    public Task<bool> DeleteMangaById(string mangaId)
    {
        throw new NotImplementedException();
    }

    public Task<List<MangaEntity>> getallManga()
    {
        throw new NotImplementedException();
    }

    public Task<MangaEntity> GetManagById(string mangaId)
    {
        throw new NotImplementedException();
    }

    public Task<MangaEntity> UpdateManga(MangaEntity entity)
    {
        throw new NotImplementedException();
    }
}
