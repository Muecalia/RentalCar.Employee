using MediatR;
using RentalCar.Employees.Application.Queries.Response.Client;
using RentalCar.Employees.Application.Wrappers;

namespace RentalCar.Employees.Application.Queries.Request.Client
{
    public class FindAllEmployeesRequest : IRequest<PagedResponse<FindAllEmployeesResponse>>
    {
    }
}
