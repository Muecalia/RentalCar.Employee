using MediatR;
using RentalCar.Employees.Application.Queries.Request.Employee;
using RentalCar.Employees.Application.Queries.Response.Client;
using RentalCar.Employees.Application.Utils;
using RentalCar.Employees.Application.Wrappers;
using RentalCar.Employees.Core.Repositories;
using RentalCar.Employees.Core.Services;

namespace RentalCar.Employees.Application.Handlers.Employees
{
    public class FindEmployeeByIdHandler : IRequestHandler<FindEmployeeByIdRequest, ApiResponse<FindEmployeeByIdResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoggerService _loggerService;
        private readonly IRabbitMqService _iRabbitMqService;

        public FindEmployeeByIdHandler(IEmployeeRepository employeeRepository, ILoggerService loggerService, IRabbitMqService iRabbitMqService)
        {
            _employeeRepository = employeeRepository;
            _loggerService = loggerService;
            _iRabbitMqService = iRabbitMqService;
        }

        public async Task<ApiResponse<FindEmployeeByIdResponse>> Handle(FindEmployeeByIdRequest request, CancellationToken cancellationToken)
        {
            const string Objecto = "employeee";
            try
            {
                var employee = await _employeeRepository.GetEmployeeDetail(request.Id, cancellationToken);
                if (employee == null) 
                {
                    _loggerService.LogWarning(MensagemError.NotFound(Objecto, request.Id));
                    return ApiResponse<FindEmployeeByIdResponse>.Error(MensagemError.NotFound(Objecto));
                }

                var result = new FindEmployeeByIdResponse(employee.Id, employee.Name, employee.Email, employee.Phone, string.Empty, employee.Gender);

                _loggerService.LogInformation(MensagemError.CarregamentoSucesso(Objecto, 1));
                return ApiResponse<FindEmployeeByIdResponse>.Success(result, MensagemError.CarregamentoSucesso(Objecto));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MensagemError.CarregamentoErro(Objecto, ex.Message));
                return ApiResponse<FindEmployeeByIdResponse>.Error(MensagemError.CarregamentoErro(Objecto));
                //throw;
            }
        }

    }
}
