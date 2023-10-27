using AcmePayLtdLibrary.Models.Request;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;

namespace AcmePayLtdAPI.Validators
{
    public class PostAuthorizeTransactionValidator : AbstractValidator<PostAuthorizeTransactionModel>
    {
        public PostAuthorizeTransactionValidator()
        {
            RuleFor(p => p.Amount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(p => p.Currency)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(receivingCurrency => ISO._4217.CurrencyCodesResolver.Codes.Any(c => c.Code == receivingCurrency)).WithMessage("Please provide a valid Currency code.");
            RuleFor(p => p.CardholderNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(BeAValidNumber).WithMessage("Please provide a valid {PropertyName}")
                .Length(16,19).WithMessage("Length ({TotalLength}) of {PropertyName} invalid"); 
            RuleFor(p => p.HolderName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(BeAValidName)
                .Length(3,100);
            RuleFor(p => p.ExpirationMonth)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(12);
            RuleFor(p => p.ExpirationYear)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.UtcNow.Year)
                .LessThanOrEqualTo(DateTime.UtcNow.Year + 50);
            RuleFor(p => p.CVV)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThanOrEqualTo(100)
                .LessThanOrEqualTo(999);
            RuleFor(p => p.OrderReference)
                .Length(0, 50);
        }

        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }

        protected bool BeAValidNumber(string number) => number.All(Char.IsDigit);
        
    }
}
