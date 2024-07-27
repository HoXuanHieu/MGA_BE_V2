using Azure.Core;
using Common;
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
    private readonly IAuthorService _authoService;
    public MangaService(IMangaRepository repository, ILogger<MangaService> logger, IUserService userService, IAuthorService authoService)
    {
        _repository = repository;
        _logger = logger;
        _userService = userService;
        _authoService = authoService;
    }

    public async Task<ApiResponse<MangaResponse>> ApproveMangaAsync(string mangaId, string modifiedBy)
    {
        var databaseItem = await _repository.GetManagByIdAsync(mangaId);
        if (databaseItem == null)
        {
            return new ApiResponse<MangaResponse>(Message.MESSAGE_MANGA_DOES_NOT_EXIST, null, 404);
        }
        var author = await _authoService.GetAuthorByIdAsync(databaseItem.AuthorId);
        if (author.Content == null)
        {
            return new ApiResponse<MangaResponse>(Message.MESSAGE_AUTHOR_DOES_NOT_EXIST, null, 404);
        }
        databaseItem.IsApproval = true;
        databaseItem.LastActivity = $"Manga has been approved by user id : {modifiedBy}";
        databaseItem.DateUpdated = DateTime.Now;
        var result = await _repository.UpdateMangaAsync(databaseItem);
        if (result == null)
        {
            return new ApiResponse<MangaResponse>(Message.MESSAGE_MANGA_UPDATE_FAIL, null, 500);
        }
        var mangaResponse = new MangaResponse(result.MangaId, result.MangaName, result.MangaImage, JsonHelper.Deserialize<List<Categories>>(databaseItem.Categories), author.Content.AuthorId, author.Content.AuthorName, result.DateUpdated);
        return new ApiResponse<MangaResponse>(Message.MESSAGE_MANGA_UPDATE_SUCCESSFUL, mangaResponse, 200);
    }

    public async Task<ApiResponse<MangaResponse>> CreateMangaAsync(CreateMangaRequest request)
    {
        // save image
        //check file extension 
        var validExtensions = new List<string>() { ".png", ".jpg", ".jfif", ".jpeg" };
        if (!FileHelper.CheckValidFileExtension(validExtensions, request.MangaImage.FileName))
            return new ApiResponse<MangaResponse>("", null, 400);
        string imageUrl = await FileHelper.SaveImageAsync(Common.Path.LOCAL_MANGA_IMAGE_STORAGE_PATH, request.MangaImage);
        //check user post exist or not ? 
        if (!await _userService.CheckUserExist(request.PostedBy))
            return new ApiResponse<MangaResponse>(Common.Message.VALIDATE_MESSAGE_USER_NOT_EXIST, null, 400);
        var author = await _authoService.GetAuthorByIdAsync(request.AuthorId);
        if (author == null)
            return new ApiResponse<MangaResponse>(Common.Message.MESSAGE_AUTHOR_DOES_NOT_EXIST, null, 400);
        var mangaEntity = new MangaEntity
        {
            MangaName = request.Title,
            MangaImage = imageUrl,
            Description = request.Description,
            Categories = "categories here",
            PostedBy = request.PostedBy,
            LastActivity = $"Create Manga by {request.PostedBy}"
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
        var response = new MangaResponse(mangaEntity.MangaId, mangaEntity.MangaName, mangaEntity.MangaImage, request.Categories, author.Content.AuthorId, author.Content.AuthorName, mangaEntity.DateUpdated);
        return new ApiResponse<MangaResponse>(result, response, 200);

    }

    public async Task<ApiResponse<bool>> DeleteMangaAsync(string mangaId)
    {
        var entity = await _repository.GetManagByIdAsync(mangaId);
        if (entity == null)
            return new ApiResponse<bool>(Common.Message.MESSAGE_MANGA_DOES_NOT_EXIST, false, 404);
        var response = await _repository.DeleteMangaByIdAsync(mangaId);
        if (response.Equals(Common.Message.MESSAGE_FILE_DELETE_SUCCESSFUL))
            return new ApiResponse<bool>(response, true, 200);
        else
            return new ApiResponse<bool>(response, false, 500);
    }

    public async Task<ApiResponse<List<MangaResponse>>> GetAllApproveAsync()
    {
        var mangas = await _repository.GetAllMangaAsync();
        var result = new List<MangaResponse>();
        if (!mangas.Any())
            return new ApiResponse<List<MangaResponse>>(Common.Message.MESSAGE_MANGA_NO_DATA, result, 203);
        var mangasHasApproval = mangas.Where(x => x.IsApproval).ToList();
        if (!mangasHasApproval.Any())
            return new ApiResponse<List<MangaResponse>>(Common.Message.MESSAGE_MANGA_NO_DATA, result, 203);
        else
        {
            foreach (var item in mangasHasApproval)
            {
                var author = await _authoService.GetAuthorByIdAsync(item.AuthorId);
                var temp = new MangaResponse(item.MangaId, item.MangaName, item.MangaImage, JsonHelper.Deserialize<List<Categories>>(item.Categories), author.Content.AuthorId, author.Content.AuthorName, item.DateUpdated);
                result.Add(temp);
            }
            return new ApiResponse<List<MangaResponse>>(Common.Message.MESSAGE_MANGA_NO_DATA, result, 200);
        }
    }

    public async Task<ApiResponse<AllMangaResponse>> GetAllMangaAsync()
    {
        var mangas = await _repository.GetAllMangaAsync();
        var result = new AllMangaResponse();
        if (!mangas.Any())
            return new ApiResponse<AllMangaResponse>(Common.Message.MESSAGE_MANGA_NO_DATA, result, 203);
        var mangaHasApproval = new List<MangaResponse>();
        var mangaNoApproval = new List<MangaResponse>();

        foreach (var item in mangas)
        {
            var author = await _authoService.GetAuthorByIdAsync(item.AuthorId);
            var itemResponse = new MangaResponse(item.MangaId, item.MangaName, item.MangaImage, JsonHelper.Deserialize<List<Categories>>(item.Categories), author.Content.AuthorId, author.Content.AuthorName, item.DateUpdated);
            if (item.IsApproval)
            {
                mangaHasApproval.Add(itemResponse);
            }
            else
            {
                mangaNoApproval.Add(itemResponse);
            }
        }
        result.mangaNoApproval = mangaNoApproval;
        result.mangaHasApproval = mangaHasApproval;
        return new ApiResponse<AllMangaResponse>(Common.Message.MESSAGE_USER_GET_SUCCESSFUL, result, 200);
    }

    public async Task<ApiResponse<MangaResponse>> GetAllMangaByAuthorAsync(string authorId)
    {
        var author = await _authoService.GetAuthorByIdAsync(authorId);
        if (author.Content == null)
            return new ApiResponse<MangaResponse>(Common.Message.MESSAGE_AUTHOR_DOES_NOT_EXIST, null, 404);
        var mangas = await _repository.GetAllMangaAsync();
        var result = new List<MangaResponse>();
        foreach (var item in mangas)
        {
            if (item.AuthorId.Equals(authorId))
            {
                var temp = new MangaResponse(item.MangaId, item.MangaName, item.MangaImage, JsonHelper.Deserialize<List<Categories>>(item.Categories), author.Content.AuthorId, author.Content.AuthorName, item.DateUpdated);
                result.Add(temp);
            }
        }
        if (!result.Any())
            return new ApiResponse<MangaResponse>(Common.Message.MESSAGE_MANGA_NO_DATA, null, 203);
        return new ApiResponse<MangaResponse>(Common.Message.MESSAGE_MANGA_GET_SUCCESSFUL, result.FirstOrDefault(), 200);
    }

    public async Task<ApiResponse<List<MangaResponse>>> GetAllMangaByUserAsync(string userId)
    {
        var user = await _userService.GetUserById(userId);
        if (user == null)
            return new ApiResponse<List<MangaResponse>>(Common.Message.VALIDATE_MESSAGE_USER_NOT_EXIST, null, 404);
        var mangas = await _repository.GetAllMangaAsync();
        var result = new List<MangaResponse>();
        foreach(var item in mangas)
        {
            if (item.PostedBy.Equals(userId)) {        
            var author = await _authoService.GetAuthorByIdAsync(item.AuthorId);
                var temp = new MangaResponse(item.MangaId, item.MangaName, item.MangaImage, JsonHelper.Deserialize<List<Categories>>(item.Categories), author.Content.AuthorId, author.Content.AuthorName, item.DateUpdated);
                result.Add(temp);
            }
        }
        if (!result.Any())
            return new ApiResponse<List<MangaResponse>>(Common.Message.MESSAGE_MANGA_NO_DATA, result, 203);
        return new ApiResponse<List<MangaResponse>>(Common.Message.MESSAGE_MANGA_GET_SUCCESSFUL, result, 200);
    }

    public async Task<ApiResponse<MangaResponse>> GetMangaByIdAsync(string mangaId)
    {
        var result = await _repository.GetManagByIdAsync(mangaId);
        if (result == null)
        {
            return new ApiResponse<MangaResponse>(Message.MESSAGE_MANGA_DOES_NOT_EXIST, null, 404);
        }
        var categories = JsonHelper.Deserialize<List<Categories>>(result.Categories);
        var author = await _authoService.GetAuthorByIdAsync(result.AuthorId);
        var mangaResponse = new MangaResponse(
            mangaId,
            result.MangaName,
            result.MangaImage,
            categories,
            author.Content == null ? "" : author.Content.AuthorId,
            author.Content == null ? "" : author.Content.AuthorName,
            result.DateUpdated);
        return new ApiResponse<MangaResponse>(Message.MESSAGE_MANGA_GET_SUCCESSFUL, mangaResponse, 200);
    }

    public async Task<ApiResponse<MangaResponse>> UpdateMangaAsync(UpdateMangaRequest request)
    {
        var databaseItem = await _repository.GetManagByIdAsync(request.MangaId);
        if (databaseItem == null)
        {
            return new ApiResponse<MangaResponse>(Message.MESSAGE_MANGA_DOES_NOT_EXIST, null, 404);
        }
        var mangaEntity = new MangaEntity()
        {
            MangaId = request.MangaId
        };
        // check author Id ? 
        var author = await _authoService.GetAuthorByIdAsync(request.AuthorId);
        if (author.Content == null)
        {
            return new ApiResponse<MangaResponse>(Message.MESSAGE_AUTHOR_DOES_NOT_EXIST, null, 404);
        }
        // check change image or not ? 
        if (!request.MangaImage.Equals(databaseItem.MangaImage))
        {
            //detele old file and create a new one
            var deleteFileResult = FileHelper.DeleteFile(databaseItem.MangaImage);
            if (deleteFileResult.Equals(Message.MESSAGE_AUTHOR_DELETE_SUCCESSFUL))
            {
                var validExtensions = new List<string>() { ".png", ".jpg", ".jfif", ".jpeg" };
                if (!FileHelper.CheckValidFileExtension(validExtensions, request.MangaImage.FileName))
                    return new ApiResponse<MangaResponse>("", null, 400);
                mangaEntity.MangaImage = await FileHelper.SaveImageAsync(Common.Path.LOCAL_MANGA_IMAGE_STORAGE_PATH , request.MangaImage);
                _logger.LogInformation(Message.MESSAGE_FILE_SAVE_SUCCESSFUL + $"Path: {mangaEntity.MangaImage}");
            }
            else
            {
                _logger.LogError(deleteFileResult);
                return new ApiResponse<MangaResponse>(Message.MESSAGE_FILE_SAVE_FAIL, null, 500);
            }
        }
        try
        {
            mangaEntity.Categories = JsonHelper.Serialize<List<Categories>>(request.Categories);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Server got error when deserialize data with message: {ex.Message}");
            return new ApiResponse<MangaResponse>(Common.Message.MESSAGE_JSON_DESERIALIZE_FAIL, null, 500);
        }
        mangaEntity.MangaName = request.Title;
        mangaEntity.Description = request.Description;
        mangaEntity.DateCreated = databaseItem.DateCreated;
        mangaEntity.DateUpdated = DateTime.Now;
        mangaEntity.IsApproval = databaseItem.IsApproval;
        mangaEntity.IsDelete = databaseItem.IsDelete;
        mangaEntity.PostedBy = databaseItem.PostedBy;
        mangaEntity.AuthorId = author.Content.AuthorId;
        mangaEntity.LastActivity = $"Update Manga information by user id: {request.ModifiedBy}"; 
        var result = await _repository.UpdateMangaAsync(mangaEntity);
        if (result == null)
        {
            return new ApiResponse<MangaResponse>(Message.MESSAGE_MANGA_UPDATE_FAIL, null, 500);
        }
        var mangaResponse = new MangaResponse(result.MangaId, result.MangaName, result.MangaImage, request.Categories, author.Content.AuthorId, author.Content.AuthorName, result.DateUpdated);
        return new ApiResponse<MangaResponse>(Message.MESSAGE_MANGA_UPDATE_SUCCESSFUL, mangaResponse, 200);
    }
}
