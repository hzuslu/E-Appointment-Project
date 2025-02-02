using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.UpdatePatient
{
    internal sealed class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Result<string>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdatePatientCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<string>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await _patientRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

            if (patient == null)
                return Result<string>.Failure("Patient not found.");

            _mapper.Map(request, patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<string>.Succeed("Patient updated successfully.");
        }
    }
}