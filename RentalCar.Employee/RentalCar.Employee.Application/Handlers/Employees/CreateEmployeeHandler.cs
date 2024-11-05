using MediatR;
using RentalCar.Employees.Application.Commands.Request.Account;
using RentalCar.Employees.Application.Commands.Request.Employee;
using RentalCar.Employees.Application.Utils;
using RentalCar.Employees.Application.Wrappers;
using RentalCar.Employees.Core.Entities;
using RentalCar.Employees.Core.Repositories;
using RentalCar.Employees.Core.Services;

namespace RentalCar.Employees.Application.Handlers.Employees
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeRequest, ApiResponse<string>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoggerService _loggerService;
        private readonly IRabbitMqService _iRabbitMqService;

        public CreateEmployeeHandler(IEmployeeRepository employeeRepository, ILoggerService loggerService, IRabbitMqService iRabbitMqService)
        {
            _employeeRepository = employeeRepository;
            _loggerService = loggerService;
            _iRabbitMqService = iRabbitMqService;
        }

        public async Task<ApiResponse<string>> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            const string Objecto = "employeee";
            const string Operacao = "criação";
            try
            {
                if (await _employeeRepository.IsEmployeeExist(request.Name, cancellationToken))
                {
                    _loggerService.LogWarning(MensagemError.Conflito($"{Objecto} {request.Name}"));
                    return ApiResponse<string>.Error(MensagemError.Conflito(Objecto));
                }
                if (await _employeeRepository.IsEmailExist(request.Email, cancellationToken))
                {
                    _loggerService.LogWarning(MensagemError.ConflitoEmail(request.Email));
                    return ApiResponse<string>.Error(MensagemError.ConflitoEmail(request.Email));
                }

                var newEmployee = new Employee 
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Gender = request.Gender                    
                };

                var employee = await _employeeRepository.Create(newEmployee, cancellationToken);

                var accountRequest = new CreateAccountRequest(employee.Name, employee.Phone, request.Role, employee.Email, request.Password, employee.Id);
                _iRabbitMqService.PublishMessage(accountRequest, RabbitQueue.NEW_ACCOUNT_QUEUE);

                return ApiResponse<string>.Success(Objecto, MensagemError.OperacaoProcessamento(Objecto, Operacao));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MensagemError.OperacaoErro(Objecto, Operacao, ex.Message));
                return ApiResponse<string>.Error(MensagemError.OperacaoErro(Objecto, Operacao));
                //throw;
            }
        }

    }
}
