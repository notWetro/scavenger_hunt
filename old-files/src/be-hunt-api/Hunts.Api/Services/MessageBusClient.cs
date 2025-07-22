using Hunts.Api.DTOs.Hunt;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Hunts.Api.Services
{
    /// <summary>
    /// A client for sending messages to the RabbitMQ message bus.
    /// </summary>
    public sealed class MessageBusClient : IMessageBusClient
    {
        private readonly IConnection? _connection;
        private readonly IModel? _channel;
        private const string EXCHANGE_NAME = "HuntExchange";

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusClient"/> class.
        /// </summary>
        /// <param name="configuration">The configuration for RabbitMQ connection.</param>
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

        /// <summary>
        /// Disposes the RabbitMQ connection and channel.
        /// </summary>
        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }

        /// <summary>
        /// Handles the connection shutdown event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The shutdown event arguments.</param>
        private void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Connection to Message Bus shutting down.");
        }

        /// <summary>
        /// Sends a message to the RabbitMQ exchange.
        /// </summary>
        /// <param name="message">The message to send.</param>
        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel?.BasicPublish(exchange: EXCHANGE_NAME, routingKey: "", basicProperties: null, body: body);
        }

        /// <summary>
        /// Publishes a deleted hunt message to the RabbitMQ exchange.
        /// </summary>
        /// <param name="hunt">The hunt data transfer object.</param>
        public void PublishDeletedHunt(HuntPublishDto hunt)
        {
            var message = JsonSerializer.Serialize(hunt);
            if (_connection is not null && _connection.IsOpen)
                SendMessage(message);
        }

        /// <summary>
        /// Publishes a new hunt message to the RabbitMQ exchange.
        /// </summary>
        /// <param name="hunt">The hunt data transfer object.</param>
        public void PublishNewHunt(HuntPublishDto hunt)
        {
            var message = JsonSerializer.Serialize(hunt);
            if (_connection is not null && _connection.IsOpen)
                SendMessage(message);
        }

        /// <summary>
        /// Publishes an updated hunt message to the RabbitMQ exchange.
        /// </summary>
        /// <param name="hunt">The hunt data transfer object.</param>
        public void PublishUpdatedHunt(HuntPublishDto hunt)
        {
            var message = JsonSerializer.Serialize(hunt);
            if (_connection is not null && _connection.IsOpen)
                SendMessage(message);
        }
    }
}
