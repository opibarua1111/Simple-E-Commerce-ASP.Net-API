using simple_ecommerce.api.Model;

namespace simple_ecommerce.api.Response
{
    public class CategoryResponse : BaseResponse
    {
        public Category Category { get; internal set; }
        public Category[] Categories { get; internal set; }
    }
}
