using eAppointmentServer.Domain.Entities;

namespace eAppointmentServer.Application
{
    public static class Constants
    {
        public static List<AppRole> GetRoles()
        {
            return [
            new(){Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString()},
            new(){Name = "Doctor", NormalizedName = "DOCTOR", ConcurrencyStamp = Guid.NewGuid().ToString()},
            new(){Name = "Patient", NormalizedName = "PATIENT", ConcurrencyStamp = Guid.NewGuid().ToString()},
            ];
        }
    }
}