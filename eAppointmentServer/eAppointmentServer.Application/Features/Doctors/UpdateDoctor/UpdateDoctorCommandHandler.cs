using AutoMapper;
using eAppointmentServer.Application.Features.Doctors.UpdateDoctor;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.UpdateDoctor
{
    internal sealed class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Result<string>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<string>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await _doctorRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

            if (doctor == null)
                return Result<string>.Failure("Doctor not found.");

            _mapper.Map(request, doctor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<string>.Succeed("Doctor updated successfully.");
        }
    }
}