using Hunts.Api.DTOs.Hunt;
using Microsoft.Identity.Client;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Hunts.Api.Services
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConnection? _connection;
        private readonly IModel? _channel;
        private const string EXCHANGE_NAME = "HuntExchange";

        public MessageBusClient(IConfiguration configuration)
        {
            var factory = new ConnectionFactory()
            { 
                HostName = configuration["RabbitMQHost"], 
                Port = int.Parse(configuration["RabbitMQPort"] ?? throw new ArgumentNullException("Port for 'RabbitMQPort' not found."))
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += OnConnectionShutdown;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't connect to Message Bus: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }

        private void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Connection to Message Bus shutting down.");
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel?.BasicPublish(exchange: EXCHANGE_NAME, routingKey: "", basicProperties: null, body: body);
        }

        public void PublishDeletedHunt(HuntPublishDto hunt)
        {
            var message = JsonSerializer.Serialize(hunt);
            if (_connection is not null && _connection.IsOpen)
                SendMessage(message);
        }

        public void PublishNewHunt(HuntPublishDto hunt)
        {
            var message = JsonSerializer.Serialize(hunt);
            if (_connection is not null && _connection.IsOpen)
                SendMessage(message);
        }

        public void PublishUpdatedHunt(HuntPublishDto hunt)
        {
            var message = JsonSerializer.Serialize(hunt);
            if (_connection is not null && _connection.IsOpen)
                SendMessage(message);
        }
    }
}
