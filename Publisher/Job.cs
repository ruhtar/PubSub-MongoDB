using Hangfire;
using RabbitMQ.Client;
using System.Text;

namespace Publisher;
public class Job : IJob
{
    private const string message = @"{
    ""pix"": [
        {
            ""txId"": ""08f714646220ea4d52ba7d3c5d05361c3a"",
            ""chave"": ""b2wa110c-eb6e-41b8-8c4d-ac4f354b03ea"",
            ""valor"": ""42.00"",
            ""horario"": ""2024-04-20T12:34:56.789Z"",
            ""pagador"": {
                ""nome"": ""Zaphod Beeblebrox"",
                ""banco"": ""Galactic Bank of Betelgeuse"",
                ""conta"": ""1234567890"",
                ""agencia"": ""42"",
                ""cpfCnpj"": ""01234567890"",
                ""tipoConta"": ""Extraterrestrial Checking"",
                ""tipoPessoa"": ""Intergalactic Being""
            },
            ""devolucoes"": [],
            ""endToEndId"": ""E306355055520240126191300352240410"",
            ""infoPagador"": ""Don't Panic!"",
            ""componentesValor"": {
                ""original"": ""42.00""
            }
        }
    ]
}";
    private const string queueName = "async-pubSub";
    private readonly ConnectionFactory _factory;
    private IConnection _connection;

    public Job()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
    }

    public void Run()
    {
        RecurringJob.AddOrUpdate(
         "my-job",
         () => Publish(),
         "*/20 * * * * *");
    }

    //Only public methods can be invoked from a Recurring Job
    public void Publish()
    {
        if (_connection == null || !_connection.IsOpen)
            _connection = _factory.CreateConnection();

        //To send, we must declare a queue for us to send to; then we can publish a message to the queue:
        //Creating the connectiong to the server
        //var factory = new ConnectionFactory { HostName = "localhost" };

        //using var connection = factory.CreateConnection();

        using var channel = _connection.CreateModel(); //Connecting to a service is slow. As time goes on, connection warms up and connections get faster. So to bypass this, RabbitMQ creates the connection and inside this connection, Channels are created. They work similar to Connection Pools of Databases and allow 

        channel.QueueDeclare(queue: queueName,
                            durable: true,
                            autoDelete: false,
                            exclusive: false,
                            arguments: null
                            );

        var body = Encoding.UTF8.GetBytes(message);
        for (int i = 0; i < 5000; i++)
        {
            channel.BasicPublish(exchange: string.Empty,
                         routingKey: queueName,
                         basicProperties: null,
                         body: body);
        }
        Console.WriteLine("Messages published.");
    }
}
