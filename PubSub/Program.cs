using PubSub.Common;
using PubSub.Publisher;
using PubSub.Subscriber;
using RabbitMQ.Client;

Console.WriteLine("Hello, World!");

// Publisher
var publisher = new PublisherService();
publisher.PublishMessage("Hello, RabbitMQ!");

// Subscriber
var subscriber = new SubscriberService();
subscriber.StartListening();

Console.Read();