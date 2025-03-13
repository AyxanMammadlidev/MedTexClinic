using FinalProject.Application.DTOs.Payment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Validators.PaymentValidator
{
    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("Card number is required.")
            .CreditCard().WithMessage("Invalid card number format.");

            RuleFor(x => x.ExpiryMonth)
                .NotEmpty().WithMessage("Expiry month is required.")
                .Matches(@"^(0[1-9]|1[0-2])$").WithMessage("Expiry month must be between 01 and 12.");

            RuleFor(x => x.ExpiryYear)
                .NotEmpty().WithMessage("Expiry year is required.")
                .Matches(@"^\d{4}$").WithMessage("Expiry year must be a valid four-digit year.");

            RuleFor(x => x.CVV)
                .NotEmpty().WithMessage("CVV is required.")
                .Matches(@"^\d{3,4}$").WithMessage("CVV must be a 3 or 4 digit number.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}
