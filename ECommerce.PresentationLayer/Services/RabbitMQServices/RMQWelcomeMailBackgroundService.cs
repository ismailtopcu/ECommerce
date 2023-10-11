using ECommerce.DtoLayer.Dtos.Messages;
using ECommerce.PresentationLayer.Services.RabbitMQEvents;
using MailKit.Net.Smtp;
using MimeKit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Web.Helpers;

namespace ECommerce.PresentationLayer.Services.RabbitMQServices
{
    public class RMQWelcomeMailBackgroundService : BackgroundService
    {
        private readonly RabbitMqClientService _rabbitmqClientService;
        private readonly ILogger<RMQWelcomeMailBackgroundService> _logger;
        private IModel _channel;
        public RMQWelcomeMailBackgroundService(RabbitMqClientService rabbitmqClientService, ILogger<RMQWelcomeMailBackgroundService> logger)
        {
            _rabbitmqClientService = rabbitmqClientService;
            _logger = logger;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitmqClientService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMqClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;

            return Task.CompletedTask;

        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var welcomeMailCreatedEvent = JsonSerializer.Deserialize<WelcomeMailCreatedEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));


                //Confirm Code kullanıcının email adresine yollanıyor.
                CreateMailDto mailDto = new();
                mailDto.Email = welcomeMailCreatedEvent.Email;
                mailDto.To = $"Sayın {welcomeMailCreatedEvent.UserName}";
                mailDto.Subject = "ModaMania'ya Hoşgeldiniz!";
                mailDto.Body = $@"
                <html>
                <head>
                    <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js""></script>
                </head>
                <body>
                    <p>Merhaba {welcomeMailCreatedEvent.NameSurname},</p>
                    <p>ModaMania'ya hoş geldiniz! Sitemize kaydolduğunuz için teşekkür ederiz.</p>
                    <p>İşte her gün yeni ürünler eklenen sitemizde ürünlerimizi kaçırmamanız için birkaç bağlantı:</p>
                    <ul>
                        <li><a href=""https://localhost:7156/"">Siteye Git</a></li>
                        <li><a href=""https://localhost:7156/Product/Index"">Ürünlere Gözat!</a></li>
                    </ul>
                    <p>ModaMania'da alışveriş yapmaya başlamak için hemen tıklayın!</p>
                </body>
                </html>
                ";

                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("ModaMania", "akademiplusecomproject@gmail.com");
                mimeMessage.From.Add(mailboxAddressFrom);
                MailboxAddress mailboxAddressTo = new MailboxAddress(mailDto.To, mailDto.Email);
                mimeMessage.To.Add(mailboxAddressTo);
                var bodyBuilder = new BodyBuilder();

                bodyBuilder.HtmlBody = mailDto.Body;
                mimeMessage.Body = bodyBuilder.ToMessageBody();

                mimeMessage.Subject = mailDto.Subject;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate("akademiplusecomproject@gmail.com", "wzqwblwshrshtvad");
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);

                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}
