using simple_ecommerce.api.Request;
using simple_ecommerce.api.Response;

namespace simple_ecommerce.api.interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponse> GetAllCategoriesAsync();
        Task<CategoryResponse> CreateCategoryAsync(AddCategoryRequest addCategoryRequest);
    }
}
