namespace RentalCar.Employees.Core.Services
{
    public interface IRabbitMqService
    {
        void PublishMessage<T>(T message, string queue);
    }
}
