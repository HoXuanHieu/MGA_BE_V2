using Common;
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

    public async Task<AuthorEntity> CreateAuthorAsync(AuthorEntity author)
    {
        try
        {
            var result = await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Create author successful.");
            return result.Entity;
        }catch (Exception ex)
        {
            _logger.LogError($"Can not create author with error: {ex.Message}.");
            return null;
        }
    }

    public async Task<string> DeleteAuthorAsync(string AuthorId)
    {
        try
        {
            _logger.LogInformation($"Create author successful.");
            var author = await GetAuthorByIdAsync(AuthorId);
            if (author == null)
                return Message.MESSAGE_AUTHOR_DOES_NOT_EXIST;
            author.IsDeleted = true;
            _context.Update(author);
            await _context.SaveChangesAsync();
            return Message.MESSAGE_AUTHOR_DELETE_SUCCESSFUL;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Can not delete author with error: {ex.Message}.");
            return Message.MESSAGE_AUTHOR_DELETE_FAIL;
        }
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
