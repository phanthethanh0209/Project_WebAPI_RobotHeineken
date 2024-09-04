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
        }

        //public async Task<bool> IsMachineCodeExist(int code)
        //{
        //    return await _repository.RecyclingMachine.AnyAsync(m => m.MachineCode == code);
        //}
    }
}
