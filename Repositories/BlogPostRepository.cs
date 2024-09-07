using Bloggie.Data;
using Bloggie.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggieDbContext.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingblog = await bloggieDbContext.Posts.FindAsync(id);
            if(existingblog != null)
            {
                bloggieDbContext.Posts.Remove(existingblog);
                await bloggieDbContext.SaveChangesAsync();
                return existingblog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.Posts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggieDbContext.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandle(string urlHandle)
        {
            return await bloggieDbContext.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var exsitingblog = await bloggieDbContext.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (exsitingblog != null) { 

                exsitingblog.Id = blogPost.Id;
                exsitingblog.Heading = blogPost.Heading;
                exsitingblog.PageTitle = blogPost.PageTitle;
                exsitingblog.Content = blogPost.Content;
                exsitingblog.ShortDescription = blogPost.ShortDescription;
                exsitingblog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                exsitingblog.UrlHandle = blogPost.UrlHandle;
                exsitingblog.PublishedDate = blogPost.PublishedDate;
                exsitingblog.Visible = blogPost.Visible;
                exsitingblog.Author = blogPost.Author;
                exsitingblog.Tags = blogPost.Tags;

                await bloggieDbContext.SaveChangesAsync();

                return exsitingblog;
            }
            return null;
        }
    }
}
