using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using simple_ecommerce.api.interfaces;
using simple_ecommerce.api.Request;

namespace simple_ecommerce.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllCategories()
        {
            var categoryResponse = await _categoryService.GetAllCategoriesAsync();

            if (!categoryResponse.Success)
            {
                return NotFound(categoryResponse);
            }

            return Ok(categoryResponse.Categories);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromForm] AddCategoryRequest addCategoryRequest)
        {
            var categoryResponse = await _categoryService.CreateCategoryAsync(addCategoryRequest);

            if (!categoryResponse.Success)
            {
                return NotFound(categoryResponse);
            }

            return Ok(categoryResponse.Category);
        }
    }
}
