using eAppointmentServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllDoctorsByDepartment
{
    public sealed record class GetAllDoctorsByDepartmentQuery : IRequest<Result<List<Doctor>>>
    {
        public int DepartmentId { get; set; }
    }
}