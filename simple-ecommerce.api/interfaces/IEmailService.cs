using simple_ecommerce.api.Model;

namespace simple_ecommerce.api.interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
