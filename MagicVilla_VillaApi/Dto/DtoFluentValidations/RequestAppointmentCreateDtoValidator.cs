using FluentValidation;
using System.Security.Claims;
namespace MagicVilla_VillaApi.Dto
{
    public class RequestAppointmentCreateDtoValidator:AbstractValidator<RequestedAppointmentCreateDto>
    {
        public RequestAppointmentCreateDtoValidator()
        {
            
            Console.WriteLine("Validation working");
            RuleFor(req => req.ClientId)
                .NotEmpty().WithMessage("Client ID Cannot Be Empty");
            RuleFor(req => req.ClientId)
                .Must(clientId => clientId > 0).WithMessage("Not Allowed");
            RuleFor(req => req.ClientId)
                .Must(id => id == Util.CommonValues.LoggedUserId).WithMessage("User Not Allowed To Make That Retuest");
            RuleFor(req => req.AppointmentId)
                .NotEmpty().WithMessage("Appointment ID Cannot Be Empty");
            RuleFor(req => req.AppointmentId)
                .Must(appoint => appoint > 0).WithMessage("Not Allowed");
        }
    }
}
