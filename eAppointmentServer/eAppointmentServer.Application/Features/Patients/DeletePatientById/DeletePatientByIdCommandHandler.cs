using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.DeleteDoctorById
{
    internal sealed class DeletePatientByIdCommandHandler : IRequestHandler<DeletePatientByIdCommand, Result<string>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeletePatientByIdCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            this._patientRepository = patientRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeletePatientByIdCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await _patientRepository.FirstOrDefaultAsync(item => item.Id == request.PatientId, cancellationToken);

            if (patient == null)
            
                return Result<string>.Failure("Patient not found.");
           

            try
            {
                _patientRepository.Delete(patient);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return Result<string>.Succeed("Patient deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An error occurred while deleting the patient: {ex.Message}");
            }

        }
    }
}