namespace RentalCar.Employees.Application.Queries.Response.Client
{
    public record FindEmployeeByIdResponse(string Id, string Name, string Email, string Phone, string Role, char Gender)
    {
    }
}
