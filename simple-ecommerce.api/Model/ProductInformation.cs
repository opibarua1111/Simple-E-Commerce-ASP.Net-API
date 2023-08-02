namespace simple_ecommerce.api.Model
{
    public class ProductInformation
    {
        public Guid? Id { get; set; }
        public string? UnitName { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? ParentCode { get; set; }
        public string? ProductBarcode { get; set; }
        public string? Description { get; set; }
        public string? SizeName { get; set; }
        public string? ColorName { get; set; }
        public string? ModelName { get; set; }
        public string? VariantName { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? Price { get; set; }
        public decimal? CostPrice { get; set; }
        public string[]? WarehouseList { get; set; }
        public decimal? Stock { get; set; }
        public decimal? TotalPurchase { get; set; }
        public string? LastPurchaseDate { get; set; }
        public string? LastPurchaseSupplier { get; set; }
        public decimal? TotalSales { get; set; }
        public string? LastSalesDate { get; set; }
        public string? LastSalesCustomer { get; set; }
        public string? ImagePath { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
    }
}
