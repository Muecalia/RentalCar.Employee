using MediatR;
using RentalCar.Employees.Application.Wrappers;

namespace RentalCar.Employees.Application.Commands.Request.Employee
{
    public class CreateEmployeeRequest : IRequest<ApiResponse<string>>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public char Gender { get; set; } = 'F';
    }
}
