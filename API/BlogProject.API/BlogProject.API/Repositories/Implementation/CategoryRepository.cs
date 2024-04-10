using BlogProject.API.Data;
using BlogProject.API.Models.Domain;
using BlogProject.API.Repositories.Interface;

namespace BlogProject.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }
    }
}
