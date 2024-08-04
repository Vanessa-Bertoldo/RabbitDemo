using RabbitDemo.Models;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace RabbitDemo.Services
{
    public class RabbitMQService
    {
        private readonly IConnectionFactory _connectionFactory;

        public RabbitMQService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void CreateQueue(string queueName)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMessage(string queueName, string message)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: System.Text.Encoding.UTF8.GetBytes(message));
        }

        public void SendMessage(MensagemModel message)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            channel.BasicPublish(exchange: "", routingKey: message.Destino, basicProperties: null, body: body);
        }


    }
}
