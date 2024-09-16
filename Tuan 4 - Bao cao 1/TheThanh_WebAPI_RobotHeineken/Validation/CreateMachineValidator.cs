using FluentValidation;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Repository;

namespace TheThanh_WebAPI_RobotHeineken.Validation
{
    public class CreateMachineValidator : AbstractValidator<CreateMachineDTO>
    {
        private IRepositoryWrapper _repository;
        public CreateMachineValidator(IRepositoryWrapper repository)
        {
            _repository = repository;

            RuleFor(m => m.MachineCode)
            .NotEmpty().WithMessage("Machine code is required");

            RuleFor(m => m.MachineName)
                .NotEmpty().WithMessage("Machine name is required");

            RuleFor(m => m.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(m => m.Capacity)
                .GreaterThan(0).WithMessage("Capacity must be greater than 0.");

            //RuleFor(m => m.LocationID)
            //    .GreaterThan(0).WithMessage("Capacity must be greater than 0.");
        }
    }
}
