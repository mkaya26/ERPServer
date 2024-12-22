using ERPServer.Application.Features.Units.CreateSystemUnit;
using ERPServer.Application.Features.Users.GetUserById;
using ERPServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERPServer.WebAPI.Controllers
{
    public class UnitController : ApiController
    {
        public UnitController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> CreateSystemUnits(CreateSystemUnitCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
