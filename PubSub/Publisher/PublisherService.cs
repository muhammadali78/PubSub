using System.Text;
using RabbitMQ.Client;
using PubSub.Common;
namespace PubSub.Publisher
{
    public class PublisherService
    {
        private readonly IModel _channel;

        public PublisherService()
        {
            var rabbitMqConnectionUriString = $"amqps://{Connection.RabbitMqUsername}:{Connection.RabbitMqPassword}@{Connection.RabbitMqServer}/{Connection.RabbitMqVhost}";
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri(rabbitMqConnectionUriString); 
            factory.AutomaticRecoveryEnabled = true;

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "message_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void PublishMessage(string content)
        {
            var messageModel = new Models.MessageModel { Content = content };
            var body = Encoding.UTF8.GetBytes(messageModel.Content);

            _channel.BasicPublish(exchange: "", routingKey: "message_queue", basicProperties: null, body: body);

            Console.WriteLine($"[Publisher] Sent message: {content}");
        }
    }
}
