using System;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

namespace Konak.EventSourcing{

    public class EventBusConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public EventBusConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ConnectionFactory getConnectionFactory(){
            return new ConnectionFactory()
                {
                    HostName = _configuration["RabbitSettings:HostName"], 
                    Port = Int32.Parse(_configuration["RabbitSettings:Port"]), 
                    UserName = _configuration["RabbitSettings:UserName"], 
                    Password = _configuration["RabbitSettings:Password"] 
                };
        }
                     
    }

}