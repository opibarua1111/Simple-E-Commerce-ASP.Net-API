using simple_ecommerce.api.Model;
using simple_ecommerce.api.Response;

namespace simple_ecommerce.api.interfaces
{
    public interface IPostService
    {
        Task SavePostImageAsync(AddProductRequest addProductRequest);
        Task UpdatePostImageAsync(UpdateProductRequest updateProductRequest);
        Task<ProductResponse> CreatePostAsync(AddProductRequest addProductRequest);
    }
}
