using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laba_3_MCV.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Travel_Blog.Context;
using Travel_Blog.Models;
using Travel_Blog_Api.Models;

namespace Travel_Blog_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            var blogs = await _blogService.GetBlogsAsync();
            return Ok(blogs);
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            try
            {
                var blog = await _blogService.GetBlogAsync(id);
                if (blog == null)
                {
                    return NotFound("Blog not found");
                }
                return Ok(blog);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // PUT: api/Blogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            if (id != blog.Id)
            {
                return BadRequest();
            }

            try
            {
                await _blogService.UpdateBlogAsync(blog);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Повертаємо повідомлення з винятку
            }

            return NoContent();
        }


        // POST: api/Blogs
        [HttpPost]
        public async Task<IActionResult> PostBlog(BlogDto blogDto)
        {
            if (blogDto == null)
            {
                return BadRequest("Blog data is null");
            }

            // Simulate fetching related data
            var blog = new Blog
            {
                Title = blogDto.Title,
                Content = blogDto.Content,
                UserId = blogDto.UserId
            };

            try
            {
                await _blogService.AddBlogAsync(blog);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blog);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                await _blogService.DeleteBlogAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Повертаємо повідомлення з винятку
            }

            return NoContent();
        }

    }
}
