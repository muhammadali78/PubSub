
using System.Text;
using PubSub.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PubSub.Subscriber
{
    public class SubscriberService
    {
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;

        public SubscriberService()
        {
            var rabbitMqConnectionUriString = $"amqps://{Connection.RabbitMqUsername}:{Connection.RabbitMqPassword}@{Connection.RabbitMqServer}/{Connection.RabbitMqVhost}";
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri(rabbitMqConnectionUriString); 
            factory.AutomaticRecoveryEnabled = true;

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "message_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageModel = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[Subscriber] Received message: {messageModel}");
            };
        }

        public void StartListening()
        {
            _channel.BasicConsume(queue: "message_queue", autoAck: true, consumer: _consumer);
            Console.WriteLine("[Subscriber] Waiting for messages. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
