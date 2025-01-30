using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Enums;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.CreateDoctor
{
    public sealed class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Result<string>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDoctorCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {

            Doctor doctor = _mapper.Map<Doctor>(request);

            try
            {
                await _doctorRepository.AddAsync(doctor, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result<string>.Succeed("Doctor created successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An error occurred while creating the doctor: {ex.Message}");
            }
        }

    }

}