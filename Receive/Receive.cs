using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel(); //Connecting to a service is slow. As time goes on, connection warms up and connections get faster. So to bypass this, RabbitMQ creates the connection and inside this connection, Channels are created. They work similar to Connection Pools of Databases and allow 

//Here in the Receive program, we are re-declaring the queue. This is no problem because RabbitMq will try to declare this queue again. If it already exists, it does nothing. Otherwise, the queue is created.
var queueName = "my-queue-name";

channel.QueueDeclare(queue: queueName,
                    durable: true,
                    autoDelete: true,
                    exclusive: false,
                    arguments: null
                    );


Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);

//This is a delegate
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
};
channel.BasicConsume(queue: queueName,
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

