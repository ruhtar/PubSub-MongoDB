using RabbitMQ.Client;
using System.Text;

//Creating the connectiong to the server
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel(); //Connecting to a service is slow. As time goes on, connection warms up and connections get faster. So to bypass this, RabbitMQ creates the connection and inside this connection, Channels are created. They work similar to Connection Pools of Databases and allow 

//To send, we must declare a queue for us to send to; then we can publish a message to the queue:
var queueName = "my-queue-name";

channel.QueueDeclare(queue: queueName,
                    durable: true,
                    autoDelete: true,
                    exclusive: false,
                    arguments: null
                    );
while (true)
{
    Console.WriteLine("Send a message... \n");
    var message = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(message)) break;

    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: string.Empty,
                         routingKey: queueName,
                         basicProperties: null,
                         body: body);
    Console.WriteLine($" [X] Sent: {message} \n");

    Console.WriteLine(" Press [enter] to exit. \n");
}