using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<AuthorRepository> _logger;
    public AuthorRepository(DatabaseContext context, ILogger<AuthorRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<AuthorEntity> CreateAuthorAsync(AuthorEntity author)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteAuthorAsync(string AuthorId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AuthorEntity>> GetAllAuthorAsync()
    {
        var response = await _context.Authors.ToListAsync();
        return response;
    }

    public async Task<AuthorEntity> GetAuthorByIdAsync(string AuthorId)
    {
        var response = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == AuthorId && !x.IsDeleted);
        return response;
    }

    public Task<AuthorEntity> UpdateAuthorAsync(AuthorEntity author)
    {
        throw new NotImplementedException();
    }
}
