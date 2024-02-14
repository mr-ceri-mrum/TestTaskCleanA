using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EncodingService;

public class EncodingController : ControllerBase
{
    private readonly EncodingService.EncodingService _encodingService = new EncodingService.EncodingService();

    public EncodingController(IPublishEndpoint publishEndpoint)
    {
        
    }

    public async Task<IActionResult> DecryptAction(string s)
    {
        await _encodingService.Decrypt(s);
        return Ok(s);
    }

    public async Task<IActionResult> EncryptAction(string s)
    {
        await _encodingService.Encrypt(s);
        return Ok(s);
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send(string routingKey, string mes)
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("amqp://rmuser:rmpassword@localhost:5672")
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        {
            channel.ExchangeDeclare(exchange: "direct_test", type: ExchangeType.Direct);

            string message = $"Message type [{routingKey}] from publisher N {mes}";

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "direct_test",
                routingKey: routingKey,
                basicProperties: null,
                body: body);
        }
        
        return Ok(routingKey);
    }

    [HttpGet("consumer")]
    public async Task<IActionResult> Consumer(string routingKey)
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("amqp://rmuser:rmpassword@localhost:5672")
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: "direct_test", type: ExchangeType.Direct);

        var queueName = channel.QueueDeclare().QueueName;

        channel.QueueBind(queue: queueName,
            exchange: "direct_test",
            routingKey: routingKey);

        var consumer = new EventingBasicConsumer(channel);
        var messageReceivedTcs = new TaskCompletionSource<string>(); // Используем TaskCompletionSource для передачи сообщения
        var list  = new List<object>();
        consumer.Received += (sender, e) =>
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            messageReceivedTcs.SetResult(message);
            // ... обработайте сообщение ...
        };
            
        // Запускаем асинхронный процесс потребления сообщений
        await Task.Run(() =>
        {
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        });
        var message = await messageReceivedTcs.Task;
            
        return Ok();
    }
}
