using BlogProject.API.Models.Domain;

namespace BlogProject.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
       Task<BlogPost> CreateAsync(BlogPost blogPost);
    }
}
