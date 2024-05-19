using Laba_3_MCV.Interfaces;
using Microsoft.EntityFrameworkCore;
using Travel_Blog.Context;
using Travel_Blog.Models;

namespace Laba_3_MCV.Repositories;

public class BlogService : IBlogService
{
    private readonly DBContext _context;

    public BlogService(DBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Blog>> GetBlogsAsync()
    {
        return await _context.Blogs.ToListAsync();
    }

    public async Task<Blog> GetBlogAsync(int id)
    {
        return await _context.Blogs.FindAsync(id);
    }

    public async Task AddBlogAsync(Blog blog)
    {
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBlogAsync(Blog blog)
    {
        _context.Entry(blog).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBlogAsync(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog != null)
        {
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }
    }
}
