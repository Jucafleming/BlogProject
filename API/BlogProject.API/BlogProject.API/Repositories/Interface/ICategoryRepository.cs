using BlogProject.API.Models.Domain;

namespace BlogProject.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
    }
}
