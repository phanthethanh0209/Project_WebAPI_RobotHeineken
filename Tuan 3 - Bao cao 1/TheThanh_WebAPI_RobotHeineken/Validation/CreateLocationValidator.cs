using FluentValidation;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Repository;

namespace TheThanh_WebAPI_RobotHeineken.Validation
{
    public class CreateLocationValidator : AbstractValidator<LocationDTO>
    {
        private readonly IRepositoryWrapper _repository;
        public CreateLocationValidator(IRepositoryWrapper repository)
        {
            _repository = repository;

            RuleFor(m => m.LocationName)
            //.Must(IsLocationNameUniqueAsync).WithMessage("Location code must be unique")
                .NotEmpty().WithMessage("Location name is required");

            RuleFor(m => m.City)
                .NotEmpty().WithMessage("City is required");

            RuleFor(m => m.District)
                .NotEmpty().WithMessage("District name is required");

            RuleFor(m => m.Ward)
                .NotEmpty().WithMessage("Ward is required");

            RuleFor(m => m.Longitude)
                .NotEmpty().WithMessage("Longitude name is required");

            RuleFor(m => m.Latitude)
                .NotEmpty().WithMessage("Latitude is required");
        }

    }
}
