using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new();

// Careful: The Uri-String should be in a json (reccomended way of doing it)
// amqp: Protocol / guest: Username / guest: Password / localhost:5672: The opened port for clients
connectionFactory.Uri = new Uri("amqp://guest:guest@localhost:5672");

connectionFactory.ClientProvidedName = "RMQDemo.Sender";

IConnection connection = connectionFactory.CreateConnection();

IModel channel = connection.CreateModel();

// RabbitMQ comes with Channels, Exchanges and Queues

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

// Create an Exchange
channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);

// Create a Queue
channel.QueueDeclare(queueName, false, false, false, null);

// Bind Exchange and Queue with the routing key (and no arguments)
channel.QueueBind(queueName, exchangeName, routingKey, null);

// Now create a message to send and publish it to the exchange
byte[] messageBodyBytes = Encoding.UTF8.GetBytes("Hello World!");
channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);

// HOW TO SEND OBJECTS?
// 1) Serialize to JSON or XML.
// 2) Encode it to UTF8 (or similar).
// 3) Send it out.

channel.Close();
connection.Close();