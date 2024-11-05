using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RentalCar.Employees.Core.Configs;
using RentalCar.Employees.Core.Services;
using System.Text;
using System.Text.Json;

namespace RentalCar.Employees.Infrastructure.MessageBus
{
    public class RabbitMqService : IRabbitMqService
    {
        private RabbitMqConfig _config { get; }

        public RabbitMqService(IOptions<RabbitMqConfig> config)
        {
            _config = config.Value;
        }

        public void PublishMessage<T>(T message, string queue)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _config.HostName,
                    UserName = _config.UserName,
                    Password = _config.Password
                };

                using var connection = factory.CreateConnection();

                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: queue,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //var json = JsonSerializer.Serialize(message);
                string json = JsonSerializer.Serialize(message, new JsonSerializerOptions { WriteIndented = true });

                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: null,
                                     body: body);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
