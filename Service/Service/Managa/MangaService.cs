using Microsoft.Extensions.Logging;
using Models;
using Models.Entities;
using Repositories;
using Service.Helper;
using Web_API.ResponseModel;

namespace Service;

public class MangaService : IMangaService
{
    private readonly IMangaRepository _repository;
    private readonly ILogger<MangaService> _logger;
    public MangaService(IMangaRepository repository, ILogger<MangaService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ApiResponse<MangaResponse>> CreateMangaAsync(CreateMangaRequest request)
    {
        // save image
        string imageUrl = await FileHelper.SaveImageAsync(Common.Path.LOCAL_IMAGE_STORAGE_PATH, request.MangaImage);
        // convert category list to string
        if (!request.Categories.Any())
            return new ApiResponse<MangaResponse>("", null, 400);
        var mangaEntity = new MangaEntity
        {
            MangaName = request.Title,
            MangaImage = imageUrl,
            Description = request.Description,
            Categories = "categories here",
            PostedBy = request.PostedBy
        };
        var result = await _repository.CreateMangaAsync(mangaEntity);
        if (result.Equals(Common.Message.MESSAGE_MANGA_CREATE_FAIL))
            return new ApiResponse<MangaResponse>(result, null, 500);
        else
        {
            List<Categories> categories = new List<Categories>();
            try
            {
                categories = JsonHelper.Deserialize<List<Categories>>(mangaEntity.Categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Server got error when deserialize data with message: {ex.Message}");
                return new ApiResponse<MangaResponse>(Common.Message.MESSAGE_JSON_DESERIALIZE_FAIL, null, 500);
            }
            var response = new MangaResponse(mangaEntity.MangaId, mangaEntity.MangaName, mangaEntity.MangaImage, categories, mangaEntity.DateUpdated);
            return new ApiResponse<MangaResponse>(result, response, 200);
        }
    }
}
