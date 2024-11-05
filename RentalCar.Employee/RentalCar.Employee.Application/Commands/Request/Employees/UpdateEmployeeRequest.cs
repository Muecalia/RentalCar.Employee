using MediatR;
using RentalCar.Employees.Application.Wrappers;

namespace RentalCar.Employees.Application.Commands.Request.Employee
{
    //public class UpdateEmployeeRequest : IRequest<ApiResponse<InputEmployeeResponse>>
    public class UpdateEmployeeRequest : IRequest<ApiResponse<string>>
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public char Gender { get; set; } = 'F';
    }
}
