using Microsoft.EntityFrameworkCore;
using simple_ecommerce.api.Data;
using simple_ecommerce.api.interfaces;
using simple_ecommerce.api.Model;
using simple_ecommerce.api.Request;
using simple_ecommerce.api.Response;

namespace simple_ecommerce.api.Services
{
    public class BrandService : IBrandService
    {
        public readonly ApplicationDBContext _context;
        public BrandService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<BrandResponse> GetAllBrandsAsync()
        {
            var brands = await _context.Brands.ToArrayAsync();
            return new BrandResponse { Success = true, Brands = brands }; 
        }
        public async Task<BrandResponse> CreateBrandAsync(AddBrandRequest addBrandRequest)
        {
            var brand = new Brand()
            {
                Id = Guid.NewGuid(),
                BrandName = addBrandRequest.BrandName,
            };
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return new BrandResponse { Success = true, Brand = brand };

        }
    }

}
