using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Participants.Api.Services
{
    /// <summary>
    /// Responsible for listening to incoming messages inside the message bus. No interface due to implementation of a background service.
    /// </summary>
    public sealed class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private readonly IConnection? _connection;
        private readonly IModel? _channel;
        private readonly string? _queueName;
        private const string EXCHANGE_NAME = "HuntExchange";

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusSubscriber"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        /// <param name="eventProcessor">The event processor to handle incoming messages.</param>
        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            this._configuration = configuration;
            this._eventProcessor = eventProcessor;

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
                _queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queue: _queueName, exchange: EXCHANGE_NAME, routingKey: "");

                _connection.ConnectionShutdown += OnConnectionShutdown;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't connect to Message Bus: {ex.Message}");
            }
        }

        /// <summary>
        /// Disposes the connection and channel.
        /// </summary>
        public override void Dispose()
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
        /// Executes the background service to listen for incoming messages.
        /// </summary>
        /// <param name="stoppingToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine("Event received");
                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(notificationMessage);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
