using Bloggie.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace Bloggie.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
