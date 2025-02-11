using eAppointmentServer.Application.Features.Roles.RoleSync;
using eAppointmentServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.Controllers
{
    [AllowAnonymous]
    public class RoleController : ApiController
    {
        public RoleController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> RoleSync(RoleSyncCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
