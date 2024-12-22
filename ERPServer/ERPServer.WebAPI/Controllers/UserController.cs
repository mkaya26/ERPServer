using ERPServer.Application.Features.Users.CreateUser;
using ERPServer.Application.Features.Users.DeleteUserById;
using ERPServer.Application.Features.Users.GetAllUsers;
using ERPServer.Application.Features.Users.GetUserById;
using ERPServer.Application.Features.Users.PasswordChangeUser;
using ERPServer.Application.Features.Users.UpdateUser;
using ERPServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERPServer.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> GetById(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
