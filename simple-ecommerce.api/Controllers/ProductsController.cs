using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simple_ecommerce.api.Data;
using simple_ecommerce.api.interfaces;
using simple_ecommerce.api.Model;

namespace simple_ecommerce.api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        public readonly ApplicationDBContext _context;
        private readonly IPostService _postService;
        public ProductsController(ApplicationDBContext context, IPostService postService)
        {
            _context = context;
            _postService = postService;
        }
    
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = (from p in _context.Products
                            join b in _context.Brands on p.BrandId equals b.Id.ToString()
                            join c in _context.Categories on p.CategoryId equals c.Id.ToString()
                            select new ProductInformation()
                            { 
                                Id = p.Id,
                                UnitName = p.UnitName,
                                Name = p.Name,
                                BrandName = b.BrandName,
                                CategoryName = c.CategoryName,
                                Description = p.Description,
                                Code = p.Code,
                                ParentCode = p.ParentCode,
                                ProductBarcode = p.ProductBarcode,
                                SizeName = p.SizeName,
                                ColorName = p.ColorName,
                                ModelName = p.ModelName,
                                VariantName = p.VariantName,
                                OldPrice = p.OldPrice,
                                Price = p.Price,
                                CostPrice = p.CostPrice,
                                WarehouseList = p.WarehouseList,
                                Stock = p.Stock,
                                TotalPurchase = p.TotalPurchase,
                                LastPurchaseDate = p.LastPurchaseDate,
                                LastPurchaseSupplier = p.LastPurchaseSupplier,
                                TotalSales = p.TotalSales,
                                LastSalesDate = p.LastSalesDate,
                                LastSalesCustomer = p.LastSalesCustomer,
                                ImagePath = p.ImagePath,
                                Type = p.Type,
                                Status = p.Status,
                            }).ToList();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult> GetProduct([FromRoute] Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                return Ok(product);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromForm] AddProductRequest addProductRequest)
        {
            if (addProductRequest.Image != null)
            {
                await _postService.SavePostImageAsync(addProductRequest);
            }

            var postResponse = await _postService.CreatePostAsync(addProductRequest);

            if (!postResponse.Success)
            {
                return NotFound(postResponse);
            }

            return Ok(postResponse.Product);
        }

        [HttpPost]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] Guid id, [FromForm] UpdateProductRequest updateProductRequest)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                if (updateProductRequest.Image != null)
                {
                    await _postService.UpdatePostImageAsync(updateProductRequest);
                    product = await _context.Products.FindAsync(id);
                    product.UnitName = updateProductRequest.UnitName;
                    product.Name = updateProductRequest.Name;
                    product.Code = updateProductRequest.Code;
                    product.ParentCode = updateProductRequest.ParentCode;
                    product.ProductBarcode = updateProductRequest.ProductBarcode;
                    product.Description = updateProductRequest.Description;
                    product.SizeName = updateProductRequest.SizeName;
                    product.ColorName = updateProductRequest.ColorName;
                    product.ModelName = updateProductRequest.ModelName;
                    product.VariantName = updateProductRequest.VariantName;
                    product.OldPrice = updateProductRequest.OldPrice;
                    product.Price = updateProductRequest.Price;
                    product.CostPrice = updateProductRequest.CostPrice;
                    product.WarehouseList = updateProductRequest.WarehouseList;
                    product.Stock = updateProductRequest.Stock;
                    product.TotalPurchase = updateProductRequest.TotalPurchase;
                    product.LastPurchaseDate = updateProductRequest.LastPurchaseDate;
                    product.LastPurchaseSupplier = updateProductRequest.LastPurchaseSupplier;
                    product.TotalSales = updateProductRequest.TotalSales;
                    product.LastSalesDate = updateProductRequest.LastSalesDate;
                    product.LastSalesCustomer = updateProductRequest.LastSalesCustomer;
                    product.ImagePath = updateProductRequest.ImagePath;
                    product.Type = updateProductRequest.Type;
                    product.Status = updateProductRequest.Status;
                    product.BrandId = updateProductRequest.BrandId;
                    product.CategoryId = updateProductRequest.CategoryId;

                    await _context.SaveChangesAsync();
                    return Ok(product);
                }
                else
                {
                    product.UnitName = updateProductRequest.UnitName;
                    product.Name = updateProductRequest.Name;
                    product.Code = updateProductRequest.Code;
                    product.ParentCode = updateProductRequest.ParentCode;
                    product.ProductBarcode = updateProductRequest.ProductBarcode;
                    product.Description = updateProductRequest.Description;
                    product.SizeName = updateProductRequest.SizeName;
                    product.ColorName = updateProductRequest.ColorName;
                    product.ModelName = updateProductRequest.ModelName;
                    product.VariantName = updateProductRequest.VariantName;
                    product.OldPrice = updateProductRequest.OldPrice;
                    product.Price = updateProductRequest.Price;
                    product.CostPrice = updateProductRequest.CostPrice;
                    product.WarehouseList = updateProductRequest.WarehouseList;
                    product.Stock = updateProductRequest.Stock;
                    product.TotalPurchase = updateProductRequest.TotalPurchase;
                    product.LastPurchaseDate = updateProductRequest.LastPurchaseDate;
                    product.LastPurchaseSupplier = updateProductRequest.LastPurchaseSupplier;
                    product.TotalSales = updateProductRequest.TotalSales;
                    product.LastSalesDate = updateProductRequest.LastSalesDate;
                    product.LastSalesCustomer = updateProductRequest.LastSalesCustomer;
                    product.ImagePath = updateProductRequest.ImagePath;
                    product.Type = updateProductRequest.Type;
                    product.Status = updateProductRequest.Status;
                    product.CategoryId = updateProductRequest.CategoryId;
                    product.BrandId = updateProductRequest.BrandId;

                    await _context.SaveChangesAsync();
                    return Ok(product);
                }
                
            }
           
            return NotFound("This id didn.t found product");
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }

            return NotFound();
        }
    }
}
