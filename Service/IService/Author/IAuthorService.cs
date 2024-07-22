using Models;
using Web_API.ResponseModel;

namespace Service;

public interface IAuthorService
{
    Task<ApiResponse<AuthorResponse>> GetAuthorByIdAsync(String AuthorId);
    Task<ApiResponse<List<AuthorResponse>>> GetAllAuthorsAsync();
    Task<ApiResponse<AuthorResponse>> CreateAuthorAsync(CreateAuthorRequest request);
    Task<ApiResponse<Boolean>> DeleteAuthorAsync(String authorId);
    Task<ApiResponse<Boolean>> UpdateAuthorAsync(UpdateAuthorRequest request);
}
