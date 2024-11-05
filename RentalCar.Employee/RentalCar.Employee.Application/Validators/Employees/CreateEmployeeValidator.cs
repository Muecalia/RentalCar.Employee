using FluentValidation;
using RentalCar.Employees.Application.Commands.Request.Employee;
using RentalCar.Employees.Application.Utils;

namespace RentalCar.Employees.Application.Validators.Employee
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeValidator() 
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Informe o nome");

            RuleFor(c => c.Role)
                .NotEmpty().WithMessage("Informe o perfil");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Informe o e-mail")
                .EmailAddress().WithMessage("Formato do e-mail inválido");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Informe a senha")
                .Length(4, 20).WithMessage("O tamanho de caracteres do nome deve estar entre 4 e 20");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Informe o nº detelefone")
                .MinimumLength(9).WithMessage("Tamanho mínimo do nº do telefone é de 9 caracteres");

            RuleFor(c => c.Gender)
                .NotEmpty().WithMessage("Informe o genero")
                .Must(GeneralService.IsvalidGender).WithMessage("Valor do genero inválido");

        }
    }
}
