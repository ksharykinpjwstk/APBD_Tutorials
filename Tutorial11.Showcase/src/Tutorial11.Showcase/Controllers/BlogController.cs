using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial11.Showcase;
using Tutorial11.Showcase.DTO;
using Tutorial11.Showcase.Models;

namespace Tutorial11.Showcase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(BloggingContext context) : ControllerBase
    {
        // GET: api/Blog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetBlogs()
        {
            var blogs =  await context.Blogs.Include(b => b.Posts).ToListAsync();
            return blogs.Select(MapToBlogDto).ToList();
        }

        // GET: api/Blog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDto>> GetBlog(int id)
        {
            var blog = await context.Blogs.Include(b => b.Posts).FirstOrDefaultAsync(b => b.BlogId == id);

            if (blog == null)
            {
                return NotFound();
            }

            return MapToBlogDto(blog);
        }

        // PUT: api/Blog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, [FromBody] UpdateBlogDto updatedBlog)
        {
            if (!BlogExists(id))
            {
                return NotFound();
            }

            var currentBlog = await context.Blogs.FirstAsync(b => b.BlogId == id);
            currentBlog.Url = updatedBlog.Url;
            
            context.Entry(currentBlog).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Blog with given ID is already being updated by another user.");
            }

            return NoContent();
        }

        // POST: api/Blog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog([FromBody] BlogDto blog)
        {
            var newBlog = new Blog
            {
                Url = blog.Url
            };
            if (blog.Posts is not null)
            {
                foreach (var post in blog.Posts)
                {
                    var newPost = new Post
                    {
                        Content = post.Content,
                        Title = post.Title,
                    };
                    newBlog.Posts.Add(newPost);
                }
            }
            context.Blogs.Add(newBlog);
            await context.SaveChangesAsync();

            return CreatedAtAction("PostBlog", new { id = newBlog.BlogId }, newBlog);
        }

        // DELETE: api/Blog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            context.Blogs.Remove(blog);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(int id)
        {
            return context.Blogs.Any(e => e.BlogId == id);
        }

        private static BlogDto MapToBlogDto(Blog blog)
        {
            return new BlogDto
            {
                Url = blog.Url,
                Posts = blog.Posts.Select(MapToPostDto).ToList()
            };
        }

        private static PostDto MapToPostDto(Post post)
        {
            return new PostDto
            {
                Title = post.Title,
                Content = post.Content
            };
        }

        private static Post MapPost(PostDto postDto, int postId)
        {
            return new Post
            {
                PostId = postId,
                Title = postDto.Title,
                Content = postDto.Content
            };
        }
    }
}
