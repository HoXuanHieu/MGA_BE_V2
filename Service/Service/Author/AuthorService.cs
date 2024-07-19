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
        var  result = new AuthorResponse()
        {
            AuthorId = data.AuthorId,
            AuthorName = data.AuthorName,
            DateUpdate = data.DateUpdate,
            Description = data.Description
        };
        return new ApiResponse<AuthorResponse>(Message.MESSAGE_AUTHOR_GET_SUCCESSUL, result, 200); 
    }
}
