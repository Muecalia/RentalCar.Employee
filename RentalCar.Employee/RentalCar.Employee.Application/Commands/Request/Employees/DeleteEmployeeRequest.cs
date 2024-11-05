using MediatR;
using RentalCar.Employees.Application.Commands.Response.Employee;
using RentalCar.Employees.Application.Wrappers;

namespace RentalCar.Employees.Application.Commands.Request.Employee
{
    public class DeleteEmployeeRequest(string id) : IRequest<ApiResponse<InputEmployeeResponse>>
    {
        public string Id { get; set; } = id;
    }
}
