using MediatR;
using RentalCar.Employees.Application.Commands.Request.Account;
using RentalCar.Employees.Application.Commands.Request.Employee;
using RentalCar.Employees.Application.Utils;
using RentalCar.Employees.Application.Wrappers;
using RentalCar.Employees.Core.Repositories;
using RentalCar.Employees.Core.Services;

namespace RentalCar.Employees.Application.Handlers.Employees
{
    //public class UpdadeEmployeeHandler : IRequestHandler<UpdateEmployeeRequest, ApiResponse<InputEmployeeResponse>>
    public class UpdadeEmployeeHandler : IRequestHandler<UpdateEmployeeRequest, ApiResponse<string>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoggerService _loggerService;
        private readonly IRabbitMqService _iRabbitMqService;

        public UpdadeEmployeeHandler(IEmployeeRepository employeeRepository, ILoggerService loggerService, IRabbitMqService iRabbitMqService)
        {
            _employeeRepository = employeeRepository;
            _loggerService = loggerService;
            _iRabbitMqService = iRabbitMqService;
        }

        public async Task<ApiResponse<string>> Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            const string Objecto = "employeee";
            const string Operacao = "editar";
            try
            {
                var employee = await _employeeRepository.GetEmployee(request.Id, cancellationToken);
                if (employee == null) 
                {
                    _loggerService.LogWarning(MensagemError.NotFound(Objecto, request.Id));
                    return ApiResponse<string>.Error(MensagemError.NotFound(Objecto));
                }

                employee.Name = request.Name;
                employee.Email = request.Email;
                employee.Phone = request.Phone;
                employee.Gender = request.Gender;

                await _employeeRepository.Update(employee, cancellationToken);

                var accountRequest = new UpdateAccountRequest(employee.Id, employee.Name, employee.Email, employee.Phone);
                _iRabbitMqService.PublishMessage(accountRequest, RabbitQueue.UPDATE_ACCOUNT_QUEUE);

                return ApiResponse<string>.Success(Objecto, MensagemError.OperacaoSucesso(Objecto, Operacao));
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
