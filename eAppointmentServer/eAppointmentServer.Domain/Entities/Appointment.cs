namespace eAppointmentServer.Domain.Entities
{
    public sealed class Appointment
    {
        public Appointment()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }


        // Navigation Properties
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}
