using Ardalis.SmartEnum;

namespace eAppointmentServer.Domain.Enums
{
    public sealed class DepartmentEnumaration : SmartEnum<DepartmentEnumaration>
    {
        public static readonly DepartmentEnumaration Cardiology = new (nameof(Cardiology), 1);
        public static readonly DepartmentEnumaration Dermatology = new (nameof(Dermatology), 2);
        public static readonly DepartmentEnumaration Gastroenterology = new (nameof(Gastroenterology), 3);
        public static readonly DepartmentEnumaration GeneralSurgery = new (nameof(GeneralSurgery), 4);
        public static readonly DepartmentEnumaration Gynecology = new (nameof(Gynecology), 5);
        public static readonly DepartmentEnumaration Neurology = new (nameof(Neurology), 6);
        public static readonly DepartmentEnumaration Ophthalmology = new (nameof(Ophthalmology), 7);
        public static readonly DepartmentEnumaration Orthopedics = new (nameof(Orthopedics), 8);
        public static readonly DepartmentEnumaration Pediatrics = new (nameof(Pediatrics), 9);
        public static readonly DepartmentEnumaration Urology = new (nameof(Urology), 10);

        public DepartmentEnumaration(string name, int value) : base(name, value)
        {
        }
    }
}