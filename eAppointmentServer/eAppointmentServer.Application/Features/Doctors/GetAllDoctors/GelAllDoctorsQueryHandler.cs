using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.GetAllDoctors
{
    internal sealed class GetAllDoctorQueryHandler : IRequestHandler<GetAllDoctorsQuery, Result<List<Doctor>>>
    {
        private readonly IDoctorRepository doctorRepository;

        public GetAllDoctorQueryHandler(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public async Task<Result<List<Doctor>>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            return await doctorRepository
                            .GetAll()
                            .OrderBy(doc => doc.Department)
                            .ThenBy(p => p.FirstName)
                            .ToListAsync(cancellationToken);
        }
    }
}