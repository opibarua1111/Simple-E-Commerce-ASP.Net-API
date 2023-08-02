using Microsoft.EntityFrameworkCore;
using simple_ecommerce.api.Data;
using simple_ecommerce.api.interfaces;
using simple_ecommerce.api.Model;
using simple_ecommerce.api.Request;
using simple_ecommerce.api.Response;

namespace simple_ecommerce.api.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly ApplicationDBContext _context;
        public CategoryService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<CategoryResponse> CreateCategoryAsync(AddCategoryRequest addCategoryRequest)
        {
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                CategoryName = addCategoryRequest.CategoryName,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return new CategoryResponse { Success = true, Category = category };
        }

        public async Task<CategoryResponse> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToArrayAsync();
            return new CategoryResponse { Success = true, Categories = categories };
        }
    }
}
