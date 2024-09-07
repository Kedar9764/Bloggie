using Bloggie.Models.Domain;

namespace Bloggie.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid BlogPostId);
        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid BlogPostId);
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}
