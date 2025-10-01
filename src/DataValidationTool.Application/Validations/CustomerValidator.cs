using DataValidationTool.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataValidationTool.Application.Validations
{
    public class CustomerValidator:AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.BirthDate)
                .NotNull()
                .LessThan(DateTime.Now).WithMessage("Birthdate must be in the past.");
        }
    }
}
