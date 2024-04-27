# PubSub System with RabbitMQ, MassTransit and MongoDB

This project consists of a Publish-Subscribe (PubSub) system that utilizes RabbitMQ as a message broker for asynchronous communication between two applications: the Publisher (Hangfire) and the Consumer. The Consumer uses MassTransit to read messages in batch and performs bulk insertion operations into MongoDB.

## Technologies

- .NET 8.0
- RabbitMQ
- MassTransit
- Hangfire (InMemory)
- MongoDB
- Docker


## Functionality

The system operates as follows:

1. **Publisher (Hangfire)**: A Hangfire recurring Job that is responsible for sending 5000 messages every 20 seconds to RabbitMQ on a specific topic.
2. **RabbitMQ**: Acts as the message broker, receiving messages from the Publisher and forwarding them to interested Consumers.
3. **Consumer**: Uses MassTransit to consume messages from RabbitMQ in batch.
4. **MongoDB**: The Consumer processes the batch of messages and performs bulk insertion operations into MongoDB.

## Components

- **Publisher**: An application that generates and sends messages to RabbitMQ.
- **Consumer**: An application that consumes messages from RabbitMQ, processes them, and performs operations on MongoDB.

## Architecture Diagram

![Architecture Diagram](https://i.ibb.co/XXdcxH1/Sem-t-tulo.png)


## Execution 

- Just run the `docker-compose` file on the root folder to raise the containers of Publisher, Consumer, MongoDB and RabbitMQ.
- Ensure that RabbitMQ is running and that all configurations are correct before starting the Publisher and Consumer services.
