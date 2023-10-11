namespace ECommerce.PresentationLayer.Services.RabbitMQEvents
{
    public class WelcomeMailCreatedEvent
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string NameSurname { get; set; }
    }
}
