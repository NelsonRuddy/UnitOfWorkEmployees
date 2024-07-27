using Domain.Entities;
using FluentValidation;


namespace Domain.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator() {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Position).NotEmpty().WithMessage("position is required");
            RuleFor(x => x.Office).NotEmpty().WithMessage("Office is required");
            RuleFor(x => x.Age).NotEmpty().WithMessage("Age is required");
            RuleFor(x => x.Salary).NotEmpty().WithMessage("Salary is required");
                
        }
    }
}
