using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Employees.Application.Commands.Request.Employee;
using RentalCar.Employees.Application.Queries.Request.Client;
using RentalCar.Employees.Application.Queries.Request.Employee;

namespace RentalCar.Employees.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin, SuperAdmin")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new FindAllEmployeesRequest(), cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpGet("getById/{id}")]
        //[Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new FindEmployeeByIdRequest(id), cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (result.Succeeded)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> Update(string id, UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var result = await _mediator.Send(request, cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteEmployeeRequest(id), cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result.Message);
        }

    }
}
