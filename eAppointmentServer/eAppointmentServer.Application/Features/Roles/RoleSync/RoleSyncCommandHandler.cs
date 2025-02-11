using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Roles.RoleSync
{
    internal sealed class RoleSyncCommandHandler : IRequestHandler<RoleSyncCommand, Result<string>>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleSyncCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result<string>> Handle(RoleSyncCommand Command, CancellationToken cancellationToken)
        {
            List<AppRole> currentRoles = await _roleManager.Roles.ToListAsync(cancellationToken);
            List<AppRole> rolesToInclude = Constants.GetRoles();

            foreach (AppRole role in currentRoles)
                if (!rolesToInclude.Any(item => item.Name == role.Name))
                    await _roleManager.DeleteAsync(role);

            foreach (AppRole role in rolesToInclude)
                if (!currentRoles.Any(item => item.Name == role.Name))
                    await _roleManager.CreateAsync(role);

            return Result<string>.Succeed("Roles have been synchronized successfully.");

        }
    }
}