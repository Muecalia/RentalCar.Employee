using MediatR;
using RentalCar.Employees.Application.Commands.Request.Employee;
using RentalCar.Employees.Application.Commands.Response.Employee;
using RentalCar.Employees.Application.Utils;
using RentalCar.Employees.Application.Wrappers;
using RentalCar.Employees.Core.Repositories;
using RentalCar.Employees.Core.Services;

namespace RentalCar.Employees.Application.Handlers.Employees
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeRequest, ApiResponse<InputEmployeeResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoggerService _loggerService;
        private readonly IRabbitMqService _iRabbitMqService;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository, ILoggerService loggerService, IRabbitMqService iRabbitMqService)
        {
            _employeeRepository = employeeRepository;
            _loggerService = loggerService;
            _iRabbitMqService = iRabbitMqService;
        }

        public async Task<ApiResponse<InputEmployeeResponse>> Handle(DeleteEmployeeRequest request, CancellationToken cancellationToken)
        {
            const string Objecto = "employeee";
            const string Operacao = "eliminar";
            try
            {
                var employee = await _employeeRepository.GetEmployeeDetail(request.Id, cancellationToken);
                if (employee == null)
                {
                    _loggerService.LogWarning(MensagemError.NotFound(Objecto, request.Id));
                    return ApiResponse<InputEmployeeResponse>.Error(MensagemError.NotFound(Objecto));
                }

                await _employeeRepository.Delete(employee, cancellationToken);

                var result = new InputEmployeeResponse(employee.Id, employee.Name, employee.Phone, employee.Email);

                return ApiResponse<InputEmployeeResponse>.Success(result, MensagemError.OperacaoSucesso(Objecto, Operacao));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MensagemError.OperacaoErro(Objecto, Operacao, ex.Message));
                return ApiResponse<InputEmployeeResponse>.Error(MensagemError.OperacaoErro(Objecto, Operacao));
                //throw;
            }
        }
    }
}
