using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Models;
using RabbitMQ.Client;

namespace MessagePublisher
{
    public class Publisher
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _channel;
        

        public void CreateConnection()
        {
            _factory = new ConnectionFactory
            {
                HostName = Constants.Messages.HostName,
                UserName = Constants.Messages.UserName,
                Password = Constants.Messages.UserName
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(Constants.Messages.ExchangeName, "direct");
            _channel.QueueDeclare(Constants.Messages.QueueName, true, false, false, null);
            _channel.QueueBind(Constants.Messages.QueueName, Constants.Messages.ExchangeName, Constants.Messages.RoutingKey);
        }

        public void SendMessages()
        {
            for (int i = 0; i < 10; i++)
            {
                var msg = CreateMessage(i);
                _channel.BasicPublish(Constants.Messages.ExchangeName, Constants.Messages.RoutingKey, null, msg.Serialize());
                Console.WriteLine("Message Published {0}", i + 1);
            }
        }

        private static Message CreateMessage(int id)
        {
            var msg = new Message() { Text = $"Message Text= {id}" };
            id = id % 3;
            if (id == 0) msg.Type = "NotificationType";
            if (id == 1) msg.Type = "PurchaseType";
            if (id == 2) msg.Type = "SalesType";
            return msg;
        }
    }
}
