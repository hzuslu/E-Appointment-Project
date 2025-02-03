using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllDoctorsByDepartment
{
    internal sealed class GetAllDoctorsByDepartmentQueryHandler : IRequestHandler<GetAllDoctorsByDepartmentQuery, Result<List<Doctor>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorRepository _doctorRepository;

        public GetAllDoctorsByDepartmentQueryHandler(IUnitOfWork unitOfWork, IDoctorRepository doctorRepository)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
        }

        public async Task<Result<List<Doctor>>> Handle(GetAllDoctorsByDepartmentQuery request, CancellationToken cancellationToken)
        {
            var doctors = await _doctorRepository.Where(item => item.Department == request.DepartmentId)
                                                 .ToListAsync(cancellationToken);
            return Result<List<Doctor>>.Succeed(doctors);
        }
    }
}
