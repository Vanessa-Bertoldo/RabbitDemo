<p align="center">
  <a href="https://www.rabbitmq.com/" target="blank"><img src="https://github.com/Vanessa-Bertoldo/RabbitDemo/blob/main/assets/rabbitmq.png" width="320" alt="Hibernate Logo" /></a>
</p>

# RabbitDemo

This project aims to demonstrate some function manipulations with Rabbit using the .NET Framework 8

RabbitMQ is an open-source messaging tool that facilitates communication between different parts of a distributed application, allowing the reliable and efficient exchange of messages between producers and consumers. It implements the AMQP (Advanced Message Queuing Protocol), though it also supports other messaging protocols

## Start project in Docket 

```bash
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
```

## Login Default
`
username: guest
`
`
password: guest
`

## Tests
To simulations use the <a href="https://tryrabbitmq.com/">rabbitmq simulator</a>
