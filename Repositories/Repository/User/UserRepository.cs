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

    public async Task<String> DeleteUserAsync(string userId)
    {
        var user = await this.GetUserByIdAsync(userId);
        if (user == null) return Common.Message.VALIDATE_MESSAGE_USER_NOT_EXIST;
        else if (user.IsDelete) return Common.Message.MESSAGE_USER_DELETE_SUCCESSFUL;
        try
        {
            user.IsDelete = true;
            _context.Users.Update(user);
            _context.SaveChanges();
            return "";
        }
        catch (Exception ex)
        {
            return Common.Message.MESSAGE_USER_DELETE_FAIL;
        }
    }

    public async Task<List<UserEntity>> GetAllUsersAsync()
    {
        return await _context.Users.Where(x => !x.IsDelete && x.IsVerify).ToListAsync();
    }

    public async Task<UserEntity> GetUserByIdAsync(string userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        return user == null ? null : user;
    }

    public async Task<String> UpdateUserAsync(UserEntity userUpdate)
    {
        try
        {
            _context.Users.Update(userUpdate);
            return Common.Message.MESSAGE_USER_UPDATE_SUCCESSFUL;
        }
        catch (Exception ex)
        {
            //logger
            return Common.Message.MESSAGE_USER_UPDATE_FAIL;
        }
    }
}
