using RabbitDemo.Models;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text.Encodings.Web;

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

        public List<MensagemModel> GetMessagesFromQueue(string queueName, int maxMessages)
        {
            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            var messages = new List<MensagemModel>();

            var consumer = new EventingBasicConsumer(channel);

            var teste = "";

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var mensagemModel = JsonConvert.DeserializeObject<MensagemModel>(message);

                var tcs = new TaskCompletionSource<bool>();

                messages.Add(mensagemModel);

                if (messages.Count >= maxMessages)
                {
                    tcs.SetResult(true); // Notifies that the desired number of messages has been reached
                }
            };

            //start consumer of messages
            channel.BasicConsume
             (
                 queue: queueName,
                 autoAck: false, // the mensage its remove of queue after read
                 consumer: consumer
             );

             while(messages.Count < maxMessages)
             {
                 //wait for messages
                 System.Threading.Thread.Sleep(100);
             }
            Console.WriteLine(teste);
            return messages;
        }

        public bool QueueExists(string queueName)
        {
            try
            {
                var connection = _connectionFactory.CreateConnection();
                var channel = connection.CreateModel();
                var result = channel.QueueDeclarePassive(queueName); //Try to declare the queue in passive mode
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
