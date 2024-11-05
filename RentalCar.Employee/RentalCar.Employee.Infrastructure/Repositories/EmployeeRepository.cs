using Microsoft.EntityFrameworkCore;
using RentalCar.Employees.Core.Entities;
using RentalCar.Employees.Core.Repositories;
using RentalCar.Employees.Infrastructure.Persistence;

namespace RentalCar.Employees.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RentalCarEmployeeContext _context;

        public EmployeeRepository(RentalCarEmployeeContext context)
        {
            _context = context;
        }

        public async Task<Employee> Create(Employee Employee, CancellationToken cancellationToken)
        {
            _context.Employee.Add(Employee);
            await _context.SaveChangesAsync(cancellationToken);
            return Employee;
        }

        public async Task Delete(Employee Employee, CancellationToken cancellationToken)
        {
            Employee.IsDeleted = true;
            Employee.DeletedAt = DateTime.UtcNow;
            _context.Employee.Update(Employee);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Employee>> GetAllEmployees(CancellationToken cancellationToken)
        {
            return await _context.Employee
                .Where(c => !c.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<Employee?> GetEmployee(string Id, CancellationToken cancellationToken)
        {
            return await _context.Employee.FirstOrDefaultAsync(c => !c.IsDeleted && string.Equals(c.Id, Id), cancellationToken);
        }

        public async Task<Employee?> GetEmployeeDetail(string Id, CancellationToken cancellationToken)
        {
            return await _context.Employee
                .FirstOrDefaultAsync(c => !c.IsDeleted && string.Equals(c.Id, Id), cancellationToken);
        }

        public async Task<bool> IsEmployeeExist(string name, CancellationToken cancellationToken)
        {
            return await _context.Employee.AnyAsync(c => string.Equals(c.Name, name), cancellationToken);
        }

        public async Task<bool> IsEmailExist(string email, CancellationToken cancellationToken)
        {
            return await _context.Employee.AnyAsync(c => string.Equals(c.Email, email), cancellationToken);
        }

        public async Task Update(Employee Employee, CancellationToken cancellationToken)
        {
            Employee.UpdatedAt = DateTime.UtcNow;
            _context.Employee.Update(Employee);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
