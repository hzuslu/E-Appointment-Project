using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.DeleteDoctorById
{
    public sealed record class DeletePatientByIdCommand : IRequest<Result<string>>
    {
        public Guid PatientId { get; set; }
    }
}