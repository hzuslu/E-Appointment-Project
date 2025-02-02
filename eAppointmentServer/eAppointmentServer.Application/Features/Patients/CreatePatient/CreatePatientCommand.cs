using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.CreatePatient
{
    public sealed record class CreatePatientCommand() : IRequest<Result<string>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Town { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
    }

}