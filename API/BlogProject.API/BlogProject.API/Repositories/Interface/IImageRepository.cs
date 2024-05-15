using BlogProject.API.Models.Domain;

namespace BlogProject.API.Repositories.Interface
{
    public interface IImageRepository
    {
       Task<BlogImage> Upload(IFormFile file, BlogImage image);
    }
}
