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
    private readonly IUserService _userService;
    public MangaService(IMangaRepository repository, ILogger<MangaService> logger, IUserService userService)
    {
        _repository = repository;
        _logger = logger;
        _userService = userService;
    }

    public async Task<ApiResponse<MangaResponse>> CreateMangaAsync(CreateMangaRequest request)
    {
        // save image
        //check file extension 
        var validExtensions = new List<string>() { ".png", ".jpg", ".jfif", ".jpeg" };
        if (!FileHelper.CheckValidFileExtension(validExtensions, request.MangaImage.FileName))
            return new ApiResponse<MangaResponse>("", null, 400);
        string imageUrl = await FileHelper.SaveImageAsync(Common.Path.LOCAL_IMAGE_STORAGE_PATH, request.MangaImage);
        //check user post exist or not ? 
        if (!await _userService.CheckUserExist(request.PostedBy))
            return new ApiResponse<MangaResponse>(Common.Message.VALIDATE_MESSAGE_USER_NOT_EXIST, null, 400);
        var mangaEntity = new MangaEntity
        {
            MangaName = request.Title,
            MangaImage = imageUrl,
            Description = request.Description,
            Categories = "categories here",
            PostedBy = request.PostedBy
        };
        try
        {
            mangaEntity.Categories = JsonHelper.Serialize<List<Categories>>(request.Categories);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Server got error when deserialize data with message: {ex.Message}");
            return new ApiResponse<MangaResponse>(Common.Message.MESSAGE_JSON_DESERIALIZE_FAIL, null, 500);
        }
        var result = await _repository.CreateMangaAsync(mangaEntity);
        if (result.Equals(Common.Message.MESSAGE_MANGA_CREATE_FAIL))
            return new ApiResponse<MangaResponse>(result, null, 500);
        var response = new MangaResponse(mangaEntity.MangaId, mangaEntity.MangaName, mangaEntity.MangaImage, request.Categories, mangaEntity.DateUpdated);
        return new ApiResponse<MangaResponse>(result, response, 200);

    }

    public async Task<ApiResponse<AllMangaResponse>> GetAllMangaAsync()
    {
        var mangas = await _repository.GetAllMangaAsync();
        var result = new AllMangaResponse();
        if (!mangas.Any())
            return new ApiResponse<AllMangaResponse>(Common.Message.MESSAGE_MANGA_NO_DATA, result, 203);
        foreach (var item in mangas)
        {
            var itemResponse = new MangaResponse(item.MangaId, item.MangaName, item.MangaImage, JsonHelper.Deserialize<List<Categories>>(item.Categories), item.DateUpdated);
            if (item.IsApproval)
            {
                result.mangaHasApproval.Add(itemResponse);
            }
            else
            {
                result.mangaNoApproval.Add(itemResponse);
            }
        }
        return new ApiResponse<AllMangaResponse>(Common.Message.MESSAGE_USER_GET_SUCCESSFUL, result, 200);
    }

    public Task<ApiResponse<List<MangaResponse>>> GetAllMangaByUserAsync(string userId)
    {
        throw new NotImplementedException();
    }
}
