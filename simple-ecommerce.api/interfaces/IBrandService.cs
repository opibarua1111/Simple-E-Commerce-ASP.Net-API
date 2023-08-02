using simple_ecommerce.api.Request;
using simple_ecommerce.api.Response;

namespace simple_ecommerce.api.interfaces
{
    public interface IBrandService
    {
        Task<BrandResponse> GetAllBrandsAsync();
        Task<BrandResponse> CreateBrandAsync(AddBrandRequest addBrandRequest);
    }
}
