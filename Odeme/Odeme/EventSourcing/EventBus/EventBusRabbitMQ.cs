using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;
using Odeme.EventSourcing;
using Odeme.Services;

namespace Odeme.EventSourcing{

    public class EventBusRabbitMQ : IEventBus
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly OdemeService _odemeService;

        public EventBusRabbitMQ(IConfiguration configuration, OdemeService odemeService)
        {
            connectionFactory = new EventBusConnectionFactory(configuration).getConnectionFactory();
            _odemeService = odemeService;
        }

        public void Publish(Event @event)
        {
            string queueName = @event.GetType().Name;
            //throw new System.NotImplementedException();
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

        public void PublishToExchange(Event @event)
        {
            string queueName = @event.GetType().Name;
            //throw new System.NotImplementedException();
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

                    channel.BasicPublish(exchange: "odeme.exchange",
                                            routingKey: "",
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
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var deserialized = new Event();
                    
                    if (eventName.Equals("UcusAyarlandiEvent"))
                    {
                        deserialized = JsonConvert.DeserializeObject<UcusAyarlandiEvent>(message);
                    }
                    
                    Type eventHandlerType = Assembly.GetEntryAssembly().GetType("Odeme.EventSourcing." + eventName + "Handler" );
                    object eventObj = Activator.CreateInstance(eventHandlerType, _odemeService, this);
                    MethodInfo method = eventHandlerType.GetMethod("Handle");

                    object[] parameters = null;

                    if (eventName.Equals("UcusAyarlandiEvent"))
                    {
                        parameters = new object[]{ (UcusAyarlandiEvent) deserialized };
                    }

                    method.Invoke(eventObj, parameters);
                    
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