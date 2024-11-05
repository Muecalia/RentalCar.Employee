using FluentValidation;
using RentalCar.Employees.Application.Commands.Request.Employee;
using RentalCar.Employees.Application.Utils;

namespace RentalCar.Employees.Application.Validators.Employee
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeValidator() 
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Informe o código do employeee");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Informe o nome");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Informe o e-mail")
                .EmailAddress().WithMessage("Formato do e-mail inválido");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Informe o nº detelefone")
                .MinimumLength(9).WithMessage("Tamanho mínimo do nº do telefone é de 9 caracteres");

            RuleFor(c => c.Gender)
                .NotEmpty().WithMessage("Informe o genero")
                .Must(GeneralService.IsvalidGender).WithMessage("Genero inválido");

        }
    }
}
