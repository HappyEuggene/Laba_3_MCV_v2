namespace Travel_Blog_Api.Models;

public class BlogDto
{
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<int> DestinationIds { get; set; } = new List<int>(); 
}
