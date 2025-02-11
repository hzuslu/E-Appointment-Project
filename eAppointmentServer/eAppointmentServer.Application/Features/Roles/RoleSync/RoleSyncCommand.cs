using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Roles.RoleSync
{
    public sealed record class RoleSyncCommand : IRequest<Result<string>>
    {
    }
}