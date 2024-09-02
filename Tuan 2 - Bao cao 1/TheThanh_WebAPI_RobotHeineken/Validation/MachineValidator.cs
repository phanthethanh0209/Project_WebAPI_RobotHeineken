using FluentValidation;
using TheThanh_WebAPI_RobotHeineken.Models;

namespace TheThanh_WebAPI_RobotHeineken.Validation
{
    public class MachineValidator : AbstractValidator<CreateMachineDTO>
    {
        public MachineValidator()
        {
            RuleFor(m => m.MachineCode)
            .NotEmpty().WithMessage("Machine code is required");

            RuleFor(m => m.MachineName)
                .NotEmpty().WithMessage("Machine name is required");

            RuleFor(m => m.Description)
                .NotEmpty().WithMessage("Description is required");

            //RuleFor(x => x.LocationID)
            //.GreaterThan(0).WithMessage("Location ID must be a positive number.");
        }
    }
}
