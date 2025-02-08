using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.UpdateAppointment
{
    public sealed record class UpdateAppointmentCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
    }

    internal sealed class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Result<string>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment appointment = await _appointmentRepository.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (appointment == null) return Result<string>.Failure("Appointment cannot be found.");

            appointment.StartDate = DateTime.Parse(request.StartDate);
            appointment.EndDate = DateTime.Parse(request.EndDate);
            _unitOfWork.SaveChanges();
            return Result<string>.Succeed("Appointment updated successfully.");
        }
    }
}