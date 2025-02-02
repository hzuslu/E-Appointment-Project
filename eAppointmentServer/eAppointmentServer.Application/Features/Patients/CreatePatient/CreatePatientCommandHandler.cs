using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.CreatePatient
{
    internal sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<string>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePatientCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {

            Patient patient = _mapper.Map<Patient>(request);

            try
            {
                await _patientRepository.AddAsync(patient, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result<string>.Succeed("Patient created successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An error occurred while creating the patient: {ex.Message}");
            }
        }

    }

}