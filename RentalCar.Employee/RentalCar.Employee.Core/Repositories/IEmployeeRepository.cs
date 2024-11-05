using RentalCar.Employees.Core.Entities;

namespace RentalCar.Employees.Core.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> Create(Employee Employee, CancellationToken cancellationToken);
        Task Update(Employee Employee, CancellationToken cancellationToken);
        Task Delete(Employee Employee, CancellationToken cancellationToken);
        Task<bool> IsEmployeeExist(string name, CancellationToken cancellationToken);
        Task<bool> IsEmailExist(string email, CancellationToken cancellationToken);
        Task<Employee?> GetEmployee(string Id, CancellationToken cancellationToken);
        Task<Employee?> GetEmployeeDetail(string Id, CancellationToken cancellationToken);
        Task<List<Employee>> GetAllEmployees(CancellationToken cancellationToken);
    }
}
