using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointments
{
    public sealed record class GetAllAppointmentsByDoctorIdQuery : IRequest<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>
    {
        public Guid DoctorId { get; set; }
    }
}