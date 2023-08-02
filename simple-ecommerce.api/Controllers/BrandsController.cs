using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using simple_ecommerce.api.interfaces;
using simple_ecommerce.api.Request;

namespace simple_ecommerce.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllBrands()
        {
            var brandResponse = await _brandService.GetAllBrandsAsync();

            if (!brandResponse.Success)
            {
                return NotFound(brandResponse);
            }

            return Ok(brandResponse.Brands);
        }

        [HttpPost]
        public async Task<ActionResult> AddBrand([FromForm] AddBrandRequest addBrandRequest)
        {
            var brandResponse = await _brandService.CreateBrandAsync(addBrandRequest);

            if (!brandResponse.Success)
            {
                return NotFound(brandResponse);
            }

            return Ok(brandResponse.Brand);
        }
    }
}
