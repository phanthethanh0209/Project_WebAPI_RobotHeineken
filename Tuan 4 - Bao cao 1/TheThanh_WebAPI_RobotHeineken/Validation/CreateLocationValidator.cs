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
                .NotEmpty().WithMessage("Location name is required");
            //.Must(CheckNameExists).WithMessage("Location name already exists");

            RuleFor(m => m.City)
                .NotEmpty().WithMessage("City is required");

            RuleFor(m => m.District)
                .NotEmpty().WithMessage("District name is required");

            RuleFor(m => m.Ward)
                .NotEmpty().WithMessage("Ward is required");

            RuleFor(m => m.Longitude)
                .NotEmpty().WithMessage("Longitude is required")
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180 degrees.");

            RuleFor(m => m.Latitude)
                .NotEmpty().WithMessage("Latitude is required")
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90 degrees.");
        }

        //private bool CheckNameExists(string locationName)
        //{
        //    return _repository.Location.AnyAsync(m => m.LocationName == locationName);
        //}
    }
}
