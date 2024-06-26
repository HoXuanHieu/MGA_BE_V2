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

    public async Task<UserEntity> CreateUserAsync(UserEntity user)
    {
        var userCreate = await _context.Users.AddAsync(user);
        if (userCreate != null)
        {
            await _context.SaveChangesAsync();
            return userCreate.Entity;
        }
        return null;
    }

    public async Task<List<UserEntity>> GetAllUsersAsync()
    {
        return await _context.Users.Where(x => !x.IsDelete).ToListAsync();
    }

}
