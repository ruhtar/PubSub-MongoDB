﻿using Microsoft.AspNetCore.Builder;
using RabbitMQ.Client;
using System.Text;


#region Vanilla RabbitMq
//Creating the connectiong to the server
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel(); //Connecting to a service is slow. As time goes on, connection warms up and connections get faster. So to bypass this, RabbitMQ creates the connection and inside this connection, Channels are created. They work similar to Connection Pools of Databases and allow 

//To send, we must declare a queue for us to send to; then we can publish a message to the queue:
const string queueName = "rabbitMq-queue";

channel.QueueDeclare(queue: queueName,
                    durable: true,
                    autoDelete: false,
                    exclusive: false,
                    arguments: null
                    );

var message = "{\r\n    \"pix\": [\r\n        {\r\n            \"txId\": \"08f714646220ea4d52ba7d3c5d05361c3a\",\r\n            \"chave\": \"b2wa110c-eb6e-41b8-8c4d-ac4f354b03ea\",\r\n            \"valor\": \"19.37\",\r\n            \"horario\": \"2024-01-26T19:13:13.157Z\",\r\n            \"pagador\": {\r\n                \"nome\": \"xxxxx.\",\r\n                \"banco\": \"xxxxx\",\r\n                \"conta\": \"xxxx\",\r\n                \"agencia\": \"1\",\r\n                \"cpfCnpj\": \"xxxxx\",\r\n                \"tipoConta\": \"xxxx\",\r\n                \"tipoPessoa\": \"xxxxxx\"\r\n            },\r\n            \"devolucoes\": [],\r\n            \"endToEndId\": \"E306355055520240126191300352240410\",\r\n            \"infoPagador\": \"Serviço realizado.\",\r\n            \"componentesValor\": {\r\n                \"original\": \"19.37\"\r\n            }\r\n        }\r\n    ]\r\n}";
//todo: adicionar publisher de 1 em 1 no loop pelo mass transit
while (true)
{
    Console.WriteLine("How many times do you want to send the message?");

    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input)) continue;
    int.TryParse(input, out var times);

    var body = Encoding.UTF8.GetBytes(message);

    for (int i = 0; i < times; i++)
    {
        channel.BasicPublish(exchange: string.Empty,
                     routingKey: queueName,
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

var app = builder.Build();

app.Run();

#endregion