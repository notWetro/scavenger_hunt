using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Here is the same code as in Sender but with a diffrent client name

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri("amqp://guest:guest@localhost:5672");
connectionFactory.ClientProvidedName = "RMQDemo.Reciever1";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);

// From here new

// 0 = prefetchSize ("We don't care how big the message is")
// 1 = prefetchCount ("How many messages should we recieve at once?")
// false = global ("Apply this just to this system or all?")
channel.BasicQos(0, 1, false);

// We are a consumer, Ssbscribe to the event when a message is received
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Recieved: {message}");

    // "We've processed the data so we acknowledge it"
    // If an exception was thrown or error happened, we can also
    // return that an error happend => This will keep message in queue
    channel.BasicAck(args.DeliveryTag, false);

    // HOW TO GET OBJECTS?
    // 1) Encode UTF8 (or similar) Encoded String. to JSON or XML.
    // 2) Derialize JSON or XML to object.
};

// Close the consumer when enter was pressed

string consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.ReadLine();

channel.BasicCancel(consumerTag);

channel.Close();
connection.Close();
