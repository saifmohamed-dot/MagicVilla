using FluentValidation;
using MagicVilla_VillaApi.Dto;

namespace MagicVilla_VillaApi.ViewModels
{
    public class RequestListVMValidator : AbstractValidator<RequestListVM>
    {
        public RequestListVMValidator()
        {
            RuleFor(vm => vm)
                .NotNull().WithMessage("Request List Cannot Be null");
            RuleFor(vm => vm.Requests)
                .NotEmpty().WithMessage("Request List Can not Be Empty");
            RuleForEach(list => list.Requests).SetValidator(new RequestAppointmentCreateDtoValidator());
        }

    }
}
