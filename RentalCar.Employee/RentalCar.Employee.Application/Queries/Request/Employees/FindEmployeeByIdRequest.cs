using MediatR;
using RentalCar.Employees.Application.Queries.Response.Client;
using RentalCar.Employees.Application.Wrappers;

namespace RentalCar.Employees.Application.Queries.Request.Employee
{
    public class FindEmployeeByIdRequest(string id) : IRequest<ApiResponse<FindEmployeeByIdResponse>>
    {
        public string Id { get; set; } = id;
    }
}
