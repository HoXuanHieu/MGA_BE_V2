using Models;
using Web_API.ResponseModel;

namespace Service;

public interface IAuthorService
{
    Task<ApiResponse<AuthorResponse>> GetAuthorByIdAsync(String AuthorId);
    Task<ApiResponse<List<AuthorResponse>>> GetAllAuthorsAsync();
}
