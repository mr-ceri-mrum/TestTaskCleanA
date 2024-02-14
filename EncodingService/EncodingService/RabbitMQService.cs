using System.Text;
using RabbitMQ.Client;

namespace EncodingService.EncodingService;

public class RabbitMQService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    
    public RabbitMQService()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost", // RabbitMQ server address
            Port = 5672,            // Default port
            UserName = "guest",     // Default username
            Password = "guest"      // Default password
        };
              
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }
    
    public void CloseConnection()
    {
        _channel.Close();
        _connection. Close();
    }
    
    public void SendMessage(string queueName, string message)
    {
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }
    
    
}