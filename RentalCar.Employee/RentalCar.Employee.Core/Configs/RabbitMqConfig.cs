﻿namespace RentalCar.Employees.Core.Configs
{
    public class RabbitMqConfig
    {
        public string HostName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
    }
}