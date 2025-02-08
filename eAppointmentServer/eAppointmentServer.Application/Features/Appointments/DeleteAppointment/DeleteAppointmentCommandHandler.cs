using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.DeleteAppointment
{
    internal class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Result<string>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await _appointmentRepository.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (appointment == null) return Result<string>.Failure("Appointment not found.");

            if (appointment.IsCompleted) return Result<string>.Failure("You can't delete completed appointment");

            _appointmentRepository.Delete(appointment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed("Appointment deleted successfully.");

        }
    }

}
