using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.DeleteDoctorById
{
    public sealed record class DeleteDoctorByIdCommand : IRequest<Result<string>>
    {
        public Guid DoctorId { get; set; }
    }
}