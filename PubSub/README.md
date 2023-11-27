# PubSub Project

This project implements a basic publisher and subscriber architecture using RabbitMQ. The publisher is responsible for sending messages, and the subscriber listens for and processes these messages.

## Project Structure

```plaintext
PubSub.Publisher
│
├── PublisherService.cs
└── Models
    └── MessageModel.cs

PubSub.Subscriber
│
├── SubscriberService.cs
└── Models
    └── MessageModel.cs

PubSub.Common
│
├── Connection.cs
