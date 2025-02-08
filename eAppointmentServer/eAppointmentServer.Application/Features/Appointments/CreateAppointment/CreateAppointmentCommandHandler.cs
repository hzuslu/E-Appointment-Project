using eAppointmentServer.Application.Features.Patients.CreatePatient;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.CreateAppointment
{
    internal class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Result<string>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CreateAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Patient patient = new();

            if (request.PatientId == null)
            {
                patient.FirstName = request.FirstName;
                patient.LastName = request.LastName;
                patient.IdentityNumber = request.IdentityNumber;
                patient.City = request.City;
                patient.Town = request.Town;
                patient.FullAddress = request.FullAddress;
            }

            await _patientRepository.AddAsync(patient, cancellationToken);

            Appointment appointment = new()
            {
                PatientId = request.PatientId ?? patient.Id,
                DoctorId = request.DoctorId,
                StartDate = DateTime.Parse(request.StartDate),
                EndDate = DateTime.Parse(request.EndDate),
                IsCompleted = false
            };

            await _appointmentRepository.AddAsync(appointment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result<string>("Appointment created successfully.");
        }
    }
}