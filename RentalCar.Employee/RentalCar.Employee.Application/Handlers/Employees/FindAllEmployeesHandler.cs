using MediatR;
using RentalCar.Employees.Application.Queries.Request.Client;
using RentalCar.Employees.Application.Queries.Response.Client;
using RentalCar.Employees.Application.Utils;
using RentalCar.Employees.Application.Wrappers;
using RentalCar.Employees.Core.Repositories;
using RentalCar.Employees.Core.Services;

namespace RentalCar.Employees.Application.Handlers.Employees
{
    public class FindAllEmployeesHandler : IRequestHandler<FindAllEmployeesRequest, PagedResponse<FindAllEmployeesResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoggerService _loggerService;

        public FindAllEmployeesHandler(IEmployeeRepository employeeRepository, ILoggerService loggerService)
        {
            _employeeRepository = employeeRepository;
            _loggerService = loggerService;
        }

        public async Task<PagedResponse<FindAllEmployeesResponse>> Handle(FindAllEmployeesRequest request, CancellationToken cancellationToken)
        {
            const string Objecto = "employee";
            try
            {
                var results = new List<FindAllEmployeesResponse>();
                var Employees = await _employeeRepository.GetAllEmployees(cancellationToken);

                Employees.ForEach(employee => results.Add(new FindAllEmployeesResponse(employee.Id, employee.Name, employee.Email, employee.Phone)));

                return new PagedResponse<FindAllEmployeesResponse>(results, MensagemError.CarregamentoSucesso(Objecto, Employees.Count));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MensagemError.CarregamentoErro(Objecto, ex.Message));
                return new PagedResponse<FindAllEmployeesResponse>(MensagemError.CarregamentoErro(Objecto));
                //throw;
            }
        }

    }
}
