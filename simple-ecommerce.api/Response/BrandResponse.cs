using simple_ecommerce.api.Model;

namespace simple_ecommerce.api.Response
{
    public class BrandResponse : BaseResponse
    {
        public Brand Brand { get; internal set; }
        public Brand[] Brands { get; internal set; }
    }
}
