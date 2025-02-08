using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetPatientById
{
    internal class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Result<Patient?>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
        {
            this._patientRepository = patientRepository;
        }

        public async Task<Result<Patient?>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            Patient? patient = await _patientRepository.FirstOrDefaultAsync(
                p => p.IdentityNumber == request.IdentityNumber,
                cancellationToken
            );

            return Result<Patient?>.Succeed(patient);
        }

    }
}