using eAppointmentServer.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace eAppointmentServer.Domain.Entities
{
    public sealed class Doctor
    {
        public Doctor()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public string FullName => string.Join(" ", FirstName, LastName);
        public DepartmentEnumaration Department { get; set; } = DepartmentEnumaration.Cardiology;
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}