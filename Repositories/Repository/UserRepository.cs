using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Linq;

namespace Repositories;

public class UserRepository : IUserRepository
{
    public readonly DatabaseContext _context;
    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateUserAsync(UserEntity user)
    {
        var userCreate = await _context.Users.AddAsync(user);
        if (userCreate != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<UserEntity>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
    
}
