using Common;
using Microsoft.Extensions.Logging;
using Models;
using Repositories;
using Web_API.ResponseModel;

namespace Service;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly ILogger<AuthorService> _logger;

    public AuthorService(IAuthorRepository repository, ILogger<AuthorService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ApiResponse<AuthorResponse>> CreateAuthorAsync(CreateAuthorRequest request)
    {
        try
        {
            var authorCreate = new AuthorEntity
            {
                AuthorId = request.AuthorId,
                AuthorName = request.AuthorName,
                Description = request.Description,
                DateCreate = DateTime.Now,
                DateUpdate = DateTime.Now,
                IsDeleted = false,
                LastActivity = "Created Author"
            };

            var author = await _repository.CreateAuthorAsync(authorCreate);
            if (author != null)
            {
                var result = new AuthorResponse()
                {
                    AuthorId = author.AuthorId,
                    AuthorName = author.AuthorName,
                    DateUpdate = author.DateUpdate,
                    Description = author.Description
                };
                return new ApiResponse<AuthorResponse>(Message.MESSAGE_AUTHOR_CREATE_SUCCESSFUL, null, 200);
            }
            return new ApiResponse<AuthorResponse>(Message.MESSAGE_AUTHOR_CREATE_FAIL, null, 500);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(Message.MESSAGE_AUTHOR_CREATE_FAIL + " With exception: " + ex.Message);
            return new ApiResponse<AuthorResponse>(Message.MESSAGE_AUTHOR_CREATE_FAIL, null, 500);
        }
    }

    public async Task<ApiResponse<bool>> DeleteAuthorAsync(string authorId)
    {
        var result = await _repository.DeleteAuthorAsync(authorId);
        if (result.Equals(Message.MESSAGE_AUTHOR_DELETE_SUCCESSFUL))
        {
            return new ApiResponse<bool>(result, true, 200);
        }
        else if (result.Equals(Message.MESSAGE_AUTHOR_DELETE_FAIL))
        {
            return new ApiResponse<bool>(result, false, 500);
        }
        else
        {
            return new ApiResponse<bool>(result, false, 404);
        }
    }

    public async Task<ApiResponse<List<AuthorResponse>>> GetAllAuthorsAsync()
    {
        var data = await _repository.GetAllAuthorAsync();
        if (!data.Any())
        {
            return new ApiResponse<List<AuthorResponse>>(Message.MESSAGE_AUTHOR_NO_DATA, null, 203);
        }
        var result = new List<AuthorResponse>();
        foreach (var item in data)
        {
            var temp = new AuthorResponse()
            {
                AuthorId = item.AuthorId,
                AuthorName = item.AuthorName,
                DateUpdate = item.DateUpdate,
                Description = item.Description
            };
            result.Add(temp);
        }
        return new ApiResponse<List<AuthorResponse>>(Message.MESSAGE_AUTHOR_GET_SUCCESSUL, result, 200);
    }

    public async Task<ApiResponse<AuthorResponse>> GetAuthorByIdAsync(string AuthorId)
    {
        var data = await _repository.GetAuthorByIdAsync(AuthorId);
        if (data == null)
            return new ApiResponse<AuthorResponse>(Message.MESSAGE_AUTHOR_DOES_NOT_EXIST, null, 404);
        var result = new AuthorResponse()
        {
            AuthorId = data.AuthorId,
            AuthorName = data.AuthorName,
            DateUpdate = data.DateUpdate,
            Description = data.Description
        };
        return new ApiResponse<AuthorResponse>(Message.MESSAGE_AUTHOR_GET_SUCCESSUL, result, 200);
    }

    public Task<ApiResponse<bool>> UpdateAuthorAsync(UpdateAuthorRequest request)
    {
        throw new NotImplementedException();
    }
}
