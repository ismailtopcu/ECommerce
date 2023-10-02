using ECommerce.DtoLayer.Dtos.Messages;

 

namespace ECommerce.WebApi.Services.EmailMailKit
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(CreateMailDto createMailDto);
    }
}
