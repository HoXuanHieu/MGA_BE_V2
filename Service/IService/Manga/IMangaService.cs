using Models;
using Web_API.ResponseModel;

namespace Service;

public interface IMangaService
{
    Task<ApiResponse<MangaResponse>> CreateMangaAsync(CreateMangaRequest request);
    Task<ApiResponse<AllMangaResponse>> GetAllMangaAsync();
    Task<ApiResponse<List<MangaResponse>>> GetAllMangaByUserAsync(String userId);
}
