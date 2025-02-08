using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.DeleteAppointment
{
    public sealed record class DeleteAppointmentCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }

}
