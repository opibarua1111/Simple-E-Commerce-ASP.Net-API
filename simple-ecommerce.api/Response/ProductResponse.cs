using simple_ecommerce.api.Model;

namespace simple_ecommerce.api.Response
{
    public class ProductResponse : ProductBaseResponse
    {
        public Product Product { get; internal set; }
    }
}
