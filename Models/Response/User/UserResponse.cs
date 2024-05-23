namespace Models.Response;

public class UserResponse
{
    public String UserId { get; set; }
    public String UserName { get; set; }
    public String Email { get; set; }
    public DateTime DateCreate { get; set; }
    public Boolean IsSuspension { get; set; }

}
