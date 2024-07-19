namespace Models;

public class AuthorResponse
{
    public String AuthorId { get; set; }
    public String AuthorName { get; set; }
    public String Description { get; set; }
    public DateTime DateUpdate { get; set; } = DateTime.Now;
}
