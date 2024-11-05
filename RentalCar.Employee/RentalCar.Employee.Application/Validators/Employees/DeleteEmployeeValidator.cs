using FluentValidation;
using RentalCar.Employees.Application.Commands.Request.Employee;

namespace RentalCar.Employees.Application.Validators.Employee
{
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeRequest>
    {
        public DeleteEmployeeValidator() 
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Informe o código do employeee");
        }
    }
}
