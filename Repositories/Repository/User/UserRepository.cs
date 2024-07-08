using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Entities;
using System.Linq;

namespace Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(DatabaseContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<String> CreateUserAsync(UserEntity user)
    {
        try
        {
            var userCreate = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Save user (user name: {user.UserName}) into database successful");
                return Common.Message.MESSAGE_USER_CREATE_SUCCESSFUL;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Server got error when save user (user name: {user.UserName}) into database: {ex.Message}"); 
            return Common.Message.MESSAGE_USER_CREATE_FAIL;
        }
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
            _logger.LogInformation($"Delete user (user name: {user.UserName}) successful");
            return "";
        }
        catch (Exception ex)
        {
            _logger.LogError($"Server got error when delete user (user name: {user.UserName}) into database: {ex.Message}");
            return Common.Message.MESSAGE_USER_DELETE_FAIL;
        }
    }

    public async Task<List<UserEntity>> GetAllUsersAsync()
    {
        _logger.LogInformation($"Start to get all users");
        return await _context.Users.Where(x => !x.IsDelete && x.IsVerify).ToListAsync();
    }

    public async Task<UserEntity> GetUserByIdAsync(string userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDelete);
        return user ?? null;
    }

    public async Task<UserEntity> GetUserByUserName(string userName)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName && !x.IsDelete);
        return user ?? null;
    }

    public async Task<String> UpdateUserAsync(UserEntity userUpdate)
    {
        try
        {
            _context.Users.Update(userUpdate);
            _context.SaveChanges();
            _logger.LogInformation($"Update user (user name: {userUpdate.UserName}) successful");
            return Common.Message.MESSAGE_USER_UPDATE_SUCCESSFUL;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Server got error when update user (user name: {userUpdate.UserName}) into database: {ex.Message}");
            return Common.Message.MESSAGE_USER_UPDATE_FAIL;
        }
    }
}
