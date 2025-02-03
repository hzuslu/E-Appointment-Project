using eAppointmentServer.Domain.Entities;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointments
{
    public sealed record class GetAllAppointmentsByDoctorIdQueryResponse
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public Patient? Patient { get; set; }
    }
}