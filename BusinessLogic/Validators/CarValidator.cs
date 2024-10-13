using DataAccess.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CarValidator:AbstractValidator<Car>
    {
        public CarValidator()
        {
           
            RuleFor(car => car.Model)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .Length(2, 50)
                .WithMessage("{PropertyName} must be between 2 and 50 characters.");

        
            RuleFor(car => car.Color)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .Length(3, 20)
                .WithMessage("{PropertyName} must be between 3 and 20 characters.");

     
            RuleFor(car => (int)car.Year)
                .InclusiveBetween(1886, DateTime.Now.Year)
                .WithMessage("{PropertyName} must be between 1886 and the current year.");

 
            RuleFor(car => car.CategoryId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be a valid category ID.");
        }
    }
}
