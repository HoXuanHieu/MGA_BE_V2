using Models;

namespace Repositories;

public interface IAuthorRepository
{
    Task<AuthorEntity> GetAuthorByIdAsync(String AuthorId);
    Task<List<AuthorEntity>> GetAllAuthorAsync();
    Task<String> DeleteAuthorAsync(String AuthorId);
    Task<AuthorEntity> UpdateAuthorAsync(AuthorEntity author);
    Task<AuthorEntity> CreateAuthorAsync(AuthorEntity author);
}
