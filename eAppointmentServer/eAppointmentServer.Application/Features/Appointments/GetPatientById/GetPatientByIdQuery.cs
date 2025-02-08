using eAppointmentServer.Domain.Entities;
using FluentValidation;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetPatientById
{
    public class GetPatientByIdQuery : IRequest<Result<Patient?>>
    {
        public string IdentityNumber { get; set; } = string.Empty;
    }

    public class GetPatientByIdQueryValidator : AbstractValidator<GetPatientByIdQuery>
    {
        public GetPatientByIdQueryValidator()
        {
            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("Identity number cannot be empty.")
                .Length(11).WithMessage("Identity number must be 11 characters long.");
        }
    }
}