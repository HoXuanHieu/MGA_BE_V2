namespace Models.Response;

public class UserResponse
{
    public String UserId { get; set; }
    public String UserName { get; set; }
    public String Email { get; set; }
    public DateTime DateCreate { get; set; }
    public Boolean IsSuspension { get; set; }
    public Boolean IsVerify {  get; set; }  

    public UserResponse()
    {
    }

    public UserResponse(string userId, string userName, string email, DateTime dateCreate, bool isSuspension, bool isVerify)
    {
        UserId = userId;
        UserName = userName;
        Email = email;
        DateCreate = dateCreate;
        IsSuspension = isSuspension;
        IsVerify = isVerify;
    }
}
