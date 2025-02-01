using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.DeleteDoctorById
{
    internal sealed class DeleteDoctorByIdCommandHandler : IRequestHandler<DeleteDoctorByIdCommand, Result<string>>
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteDoctorByIdCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            this.doctorRepository = doctorRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository.FirstOrDefaultAsync(item => item.Id == request.DoctorId, cancellationToken);

            if (doctor == null)
            
                return Result<string>.Failure("Doctor not found.");
           

            try
            {
                doctorRepository.Delete(doctor);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return Result<string>.Succeed("Doctor deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An error occurred while deleting the doctor: {ex.Message}");
            }

        }
    }
}