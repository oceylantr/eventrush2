using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;

namespace Ucus.EventSourcing{

    public class EventBusRabbitMQ : IEventBus
    {
        private readonly ConnectionFactory connectionFactory;

        public EventBusRabbitMQ(IConfiguration configuration)
        {
            connectionFactory = new EventBusConnectionFactory(configuration).getConnectionFactory();
        }

        public void Publish(Event @event)
        {
            string queueName = @event.GetType().Name;
            
            using (var connection = connectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

                    string message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                            routingKey: queueName,
                                            basicProperties: null,
                                            body: body);
                }
        }

        public void Subscribe(string eventName)
        {
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            {
                channel.QueueDeclare(queue: eventName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {

                };
                channel.BasicConsume(queue: eventName,
                                        autoAck: true,
                                        consumer: consumer);

            }
        }

        public void Unsubscribe(string eventName)
        {
            throw new System.NotImplementedException();
        }
    }

}