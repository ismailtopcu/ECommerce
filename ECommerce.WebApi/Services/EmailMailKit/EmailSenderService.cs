using ECommerce.DtoLayer.Dtos.Messages;
using MailKit.Net.Smtp;
using MimeKit;


namespace ECommerce.WebApi.Services.EmailMailKit
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(CreateMailDto createMailDto)
        {
        MimeMessage mimeMessage = new MimeMessage();
        MailboxAddress mailboxAddressFrom = new MailboxAddress("ModaMania", "akademiplusecomproject@gmail.com");
        mimeMessage.From.Add(mailboxAddressFrom);
        MailboxAddress mailboxAddressTo = new MailboxAddress(createMailDto.To, createMailDto.Email);
        mimeMessage.To.Add(mailboxAddressTo);
        var bodyBuilder = new BodyBuilder();

        bodyBuilder.HtmlBody = createMailDto.Body;
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        mimeMessage.Subject = createMailDto.Subject;
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Connect("smtp.gmail.com", 587, false);
        smtpClient.Authenticate("akademiplusecomproject@gmail.com", "wzqwblwshrshtvad");
        smtpClient.Send(mimeMessage);
        smtpClient.Disconnect(true);
        
            return Task.CompletedTask;
        }
    }
}
