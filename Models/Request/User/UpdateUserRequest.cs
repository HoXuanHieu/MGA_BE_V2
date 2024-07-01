using System.ComponentModel.DataAnnotations;

namespace Models;

public class UpdateUserRequest
{
    public string UserId { get; set; }  
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public Boolean IsSuspension { get; set; }
    public Boolean IsVerify { get; set; }
    public Boolean IsDelete { get; set; }
    public DateTime DateUpdate { get; set; } = DateTime.Now;
}
