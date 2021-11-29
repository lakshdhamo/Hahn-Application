using FluentValidation;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Validators
{
    /// <summary>
    /// Validates the User model
    /// </summary>
    public class UserValidator : AbstractValidator<UserVm>
    {
        public UserValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please ensure you have entered the First Name").MinimumLength(3).WithMessage("First Name should contains at least 3 Characters");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Please ensure you have entered the Last Name").MinimumLength(3).WithMessage("Last Name should contains at least 3 Characters");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Please ensure you have entered the Adress").MinimumLength(10).WithMessage("Adress should contains at least 10 Characters");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Please ensure you have entered the Email")
                .EmailAddress().WithMessage("EmailAdress - must be an valid email");
            RuleFor(x => x.Age).NotEmpty().GreaterThan(18).WithMessage("Age must be greater than 18"); ;
        }
    }
}
