using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.UpdatePatient
{
    public sealed record class UpdatePatientCommand() : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Town { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
    }
}