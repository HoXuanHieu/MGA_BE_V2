using Models;
using Web_API.ResponseModel;

namespace Service;

public interface IMangaService
{
    Task<ApiResponse<MangaResponse>> CreateMangaAsync(CreateMangaRequest request);

}
