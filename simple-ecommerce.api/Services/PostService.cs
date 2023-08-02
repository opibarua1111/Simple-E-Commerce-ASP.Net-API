using simple_ecommerce.api.Data;
using simple_ecommerce.api.interfaces;
using simple_ecommerce.api.Model;
using simple_ecommerce.api.Response;

namespace simple_ecommerce.api.Services
{
    public class PostService : IPostService
    {
        private readonly IWebHostEnvironment _environment;

        public readonly ApplicationDBContext _context;
        public PostService(ApplicationDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<ProductResponse> CreatePostAsync(AddProductRequest addProductRequest)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                UnitName = addProductRequest.UnitName,
                Name = addProductRequest.Name,
                Code = addProductRequest.Code,
                ParentCode = addProductRequest.ParentCode,
                ProductBarcode = addProductRequest.ProductBarcode,
                Description = addProductRequest.Description,
                SizeName = addProductRequest.SizeName,
                ColorName = addProductRequest.ColorName,
                ModelName = addProductRequest.ModelName,
                VariantName = addProductRequest.VariantName,
                OldPrice = addProductRequest.OldPrice,
                Price = addProductRequest.Price,
                CostPrice = addProductRequest.CostPrice,
                WarehouseList = addProductRequest.WarehouseList,
                Stock = addProductRequest.Stock,
                TotalPurchase = addProductRequest.TotalPurchase,
                LastPurchaseDate = addProductRequest.LastPurchaseDate,
                LastPurchaseSupplier = addProductRequest.LastPurchaseSupplier,
                TotalSales = addProductRequest.TotalSales,
                LastSalesDate = addProductRequest.LastSalesDate,
                LastSalesCustomer = addProductRequest.LastSalesCustomer,
                ImagePath = addProductRequest.ImagePath,
                Type = addProductRequest.Type,
                Status = addProductRequest.Status,
                BrandId = addProductRequest.BrandId,
                CategoryId = addProductRequest.CategoryId,

            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return new ProductResponse { Success = true, Product = product};

        }

        public async Task SavePostImageAsync(AddProductRequest addProductRequest)
        {
            var uniqueFileName = GetUniqueFileName(addProductRequest.Image.FileName);

            var uploads = Path.Combine(_environment.WebRootPath, "ProductImages");

            var filePath = Path.Combine(uploads, uniqueFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            await addProductRequest.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var newFilePath = Path.Combine("ProductImages", uniqueFileName);

            addProductRequest.ImagePath = newFilePath;

            return;
        }
        public async Task UpdatePostImageAsync(UpdateProductRequest updateProductRequest)
        {
            var uniqueFileName = GetUniqueFileName(updateProductRequest.Image.FileName);

            var uploads = Path.Combine(_environment.WebRootPath, "ProductImages");

            var filePath = Path.Combine(uploads, uniqueFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            await updateProductRequest.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var newFilePath = Path.Combine("ProductImages", uniqueFileName);

            updateProductRequest.ImagePath = newFilePath;

            return;
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return string.Concat(Path.GetFileNameWithoutExtension(fileName)
                                , "_"
                                , Guid.NewGuid().ToString().AsSpan(0, 4)
                                , Path.GetExtension(fileName));
        }
    }
}
