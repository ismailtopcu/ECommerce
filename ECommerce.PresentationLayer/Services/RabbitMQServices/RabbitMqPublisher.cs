using ECommerce.PresentationLayer.Services.RabbitMQEvents;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ECommerce.PresentationLayer.Services.RabbitMQServices
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMqClientService _rabbitmqClientService;

        public RabbitMqPublisher(RabbitMqClientService rabbitmqClientService)
        {
            _rabbitmqClientService = rabbitmqClientService;
        }
        public void Publish(WelcomeMailCreatedEvent mailCreatedEvent)
        {
            var channel = _rabbitmqClientService.Connect();

            var bodyString = JsonSerializer.Serialize(mailCreatedEvent);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMqClientService.ExchangeName, routingKey: RabbitMqClientService.UserIdentity, basicProperties: properties, body: bodyByte);
        }
    }
}
