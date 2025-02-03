using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointments
{
    internal sealed class GetAllAppointmentsByDoctorIdQueryHandler : IRequestHandler<GetAllAppointmentsByDoctorIdQuery, Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAllAppointmentsByDoctorIdQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            List<GetAllAppointmentsByDoctorIdQueryResponse> appointmentList = await _appointmentRepository
                .Where(app => app.DoctorId == request.DoctorId)
                .Include(app => app.Patient)
                .Select(item => new GetAllAppointmentsByDoctorIdQueryResponse
                {
                    Id = item.Id,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Title = item.Patient!.FullName,
                    Patient = item.Patient
                })
                .ToListAsync(cancellationToken);

            return Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>.Succeed(appointmentList);
        }
    }
}