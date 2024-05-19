using Travel_Blog.Models;

namespace Laba_3_MCV.Interfaces;

public interface IBlogService
{
    Task<IEnumerable<Blog>> GetBlogsAsync();
    Task<Blog> GetBlogAsync(int id);
    Task AddBlogAsync(Blog blog);
    Task UpdateBlogAsync(Blog blog);
    Task DeleteBlogAsync(int id);
}
