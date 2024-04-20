using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Receive;
using System.Text;


var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel(); //Connecting to a service is slow. As time goes on, connection warms up and connections get faster. So to bypass this, RabbitMQ creates the connection and inside this connection, Channels are created. They work similar to Connection Pools of Databases and allow 

//Here in the Receive program, we are re-declaring the queue. This is no problem because RabbitMq will try to declare this queue again. If it already exists, it does nothing. Otherwise, the queue is created.
const string queueName = "my-queue-name";

channel.QueueDeclare(queue: queueName,
                    durable: true,
                    autoDelete: false,
                    exclusive: false,
                    arguments: null
                    );


Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(queue: queueName,
                     autoAck: true,
                     consumer: consumer);

//This is a delegate
consumer.Received += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var repo = new MongoRepository();
    await repo.InsertJsonAsync(message);
    Console.WriteLine($" [x] Received: {message}");
};


Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

