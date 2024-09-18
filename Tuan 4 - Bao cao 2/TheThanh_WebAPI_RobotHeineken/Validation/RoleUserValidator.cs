using FluentValidation;
using TheThanh_WebAPI_RobotHeineken.Models;

namespace TheThanh_WebAPI_RobotHeineken.Validation
{
    public class RoleUserValidator : AbstractValidator<RoleUserDTO>
    {
        public RoleUserValidator()
        {
            RuleFor(role => role.RoleID)
               .NotEmpty().WithMessage("Role ID is required.");

            RuleFor(role => role.UserID)
                .NotEmpty().WithMessage("User ID is required.");
        }

    }
}
