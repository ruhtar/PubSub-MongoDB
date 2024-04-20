using Microsoft.AspNetCore.Builder;
using Publisher;
using RabbitMQ.Client;
using System.Text;


#region Vanilla RabbitMq
//Creating the connectiong to the server
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel(); //Connecting to a service is slow. As time goes on, connection warms up and connections get faster. So to bypass this, RabbitMQ creates the connection and inside this connection, Channels are created. They work similar to Connection Pools of Databases and allow 

//To send, we must declare a queue for us to send to; then we can publish a message to the queue:
const string vanillaQueueName = "queue-mass-transit";

channel.QueueDeclare(queue: vanillaQueueName,
                    durable: true,
                    autoDelete: false,
                    exclusive: false,
                    arguments: null
                    );

var message = @"{
    ""pix"": [
        {
            ""txId"": ""08f714646220ea4d52ba7d3c5d05361c3a"",
            ""chave"": ""b2wa110c-eb6e-41b8-8c4d-ac4f354b03ea"",
            ""valor"": ""19.37"",
            ""horario"": ""2024-01-26T19:13:13.157Z"",
            ""pagador"": {
                ""nome"": ""xxxxx"",
                ""banco"": ""xxxxx"",
                ""conta"": ""xxxx"",
                ""agencia"": ""1"",
                ""cpfCnpj"": ""xxxxx"",
                ""tipoConta"": ""xxxx"",
                ""tipoPessoa"": ""xxxxxx""
            },
            ""devolucoes"": [],
            ""endToEndId"": ""E306355055520240126191300352240410"",
            ""infoPagador"": ""Serviço realizado."",
            ""componentesValor"": {
                ""original"": ""19.37""
            }
        }
    ]
}";

//todo: adicionar publisher de 1 em 1 no loop pelo mass transit
while (true)
{
    Console.WriteLine("How many times do you want to send the message?");

    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input)) continue;
    int.TryParse(input, out var times);

    var body = Encoding.UTF8.GetBytes(message);

    for (int i = 0; i < times * 2000; i++)
    {
        channel.BasicPublish(exchange: string.Empty,
                     routingKey: vanillaQueueName,
                     basicProperties: null,
                     body: body);
    }

    Console.WriteLine($" [X] Sent: {message} \n");

    Console.WriteLine("Press [q] to exit. \n");
    var command = Console.ReadLine();
    if (command == "q") break;
}

#endregion

#region Mass transit

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransitConfiguration();

var app = builder.Build();

app.Run();

#endregion