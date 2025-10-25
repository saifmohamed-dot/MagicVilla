using FluentValidation;
using MagicVilla_Web.DTO;

namespace MagicVilla_Web.DtoFluentValidations
{
    public class RequestAppointmentCreateDtoValidator:AbstractValidator<RequestAppointmentCreateDto>
    {
        public RequestAppointmentCreateDtoValidator()
        {
            Console.WriteLine("Validation working");
            RuleFor(req => req.ClientId)
                .NotEmpty().WithMessage("Client ID Cannot Be Empty");
            RuleFor(req => req.ClientId)
                .Must(clientId => clientId > 0).WithMessage("Not Allowed");
            RuleFor(req => req.AppointmentId)
                .NotEmpty().WithMessage("Appointment ID Cannot Be Empty");
            RuleFor(req => req.AppointmentId)
                .Must(appoint => appoint > 0).WithMessage("Not Allowed");
        }
    }
}
