using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.GetAllPatients
{
    internal sealed class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, Result<List<Patient>>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetAllPatientsQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Result<List<Patient>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            return await _patientRepository
                            .GetAll()
                            .OrderBy(patient => patient.FirstName)
                            .ToListAsync(cancellationToken);
        }
    }
}