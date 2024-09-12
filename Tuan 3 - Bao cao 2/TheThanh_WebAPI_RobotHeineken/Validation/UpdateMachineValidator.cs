using FluentValidation;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Repository;

namespace TheThanh_WebAPI_RobotHeineken.Validation
{
    public class UpdateMachineValidator : AbstractValidator<UpdateMachineDTO>
    {
        IRepositoryWrapper _repository;
        public UpdateMachineValidator(IRepositoryWrapper repository)
        {
            _repository = repository;

            RuleFor(m => m.MachineCode)
            .NotEmpty().WithMessage("Machine code is required");

            RuleFor(m => m.MachineName)
                .NotEmpty().WithMessage("Machine name is required");

            RuleFor(m => m.Description)
                .NotEmpty().WithMessage("Description is required");


            //RuleFor(x => x.LocationID)
            //.GreaterThan(0).WithMessage("Location ID must be a positive number.");
        }

        public async Task<bool> IsMachineInOperation(int code)
        {
            return await _repository.RecyclingMachine.AnyAsync(m => m.MachineCode == code);
        }
    }
}
