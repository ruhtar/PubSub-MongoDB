using Consumer.Extensions;
using Microsoft.AspNetCore.Builder;


#region Vanilla RabbitMq
//var factory = new ConnectionFactory { HostName = "localhost" };
//using var connection = factory.CreateConnection();
//using var channel = connection.CreateModel(); //Connecting to a service is slow. As time goes on, connection warms up and connections get faster. So to bypass this, RabbitMQ creates the connection and inside this connection, Channels are created. They work similar to Connection Pools of Databases and allow 

////Here in the Receive program, we are re-declaring the queue. This is no problem because RabbitMq will try to declare this queue again. If it already exists, it does nothing. Otherwise, the queue is created.
//const string vanillaQueueName = "rabbitMq-queue";

//channel.QueueDeclare(queue: vanillaQueueName,
//                    durable: true,
//                    autoDelete: false,
//                    exclusive: false,
//                    arguments: null
//                    );


//Console.WriteLine(" [*] Waiting for messages.");

//var consumer = new EventingBasicConsumer(channel);

//channel.BasicConsume(queue: vanillaQueueName,
//                     autoAck: true,
//                     consumer: consumer);

////This is a delegate
////todo: adicionar consumer em batch e implementar metodo de insert em massa no mongo
//consumer.Received += async (model, ea) =>
//{
//    var body = ea.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    var repo = new MongoRepository();
//    await repo.InsertJsonAsync(message);
//    Console.WriteLine($" [x] Received: {message}");
//};


//Console.WriteLine(" Press [enter] to exit.");
//Console.ReadLine();

#endregion

#region Mass transit

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransitConfiguration();
builder.Services.AddRepository();
builder.Services.AddIOptionsImplementation(builder.Configuration);

var app = builder.Build();

app.Run();

#endregion