namespace RentalCar.Employees.Infrastructure.ExternalApi.Security
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string IdUser { get; set; } = string.Empty;
        public bool IsEmployee { get; set; } = true;
    }
}
