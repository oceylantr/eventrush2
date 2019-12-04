using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;
using Konak.EventSourcing;
using Konak.Services;

namespace Konak.EventSourcing{

    public class EventBusRabbitMQ : IEventBus
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly KonakService _konakService;

        public EventBusRabbitMQ(IConfiguration configuration, KonakService konakService)
        {
            connectionFactory = new EventBusConnectionFactory(configuration).getConnectionFactory();
            _konakService = konakService;
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

        public void Subscribe(string eventName)
        {
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            {

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var deserialized = JsonConvert.DeserializeObject<OdemeBasariliOlduEvent>(message);
                    
                    Type eventHandlerType = Assembly.GetEntryAssembly().GetType("Konak.EventSourcing.OdemeBasariliOlduEventHandler" );
                    object eventObj = Activator.CreateInstance(eventHandlerType, _konakService, this);
                    MethodInfo method = eventHandlerType.GetMethod("Handle");

                    object[] parameters = new object[]{ (OdemeBasariliOlduEvent) deserialized };

                    method.Invoke(eventObj, parameters);
                    
                    //channel.BasicAck(ea.DeliveryTag, false);
                };
                //channel.BasicQos(0, 1, false);
                channel.BasicConsume(queue: "KonaklamaTalepEdildiEvent",
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