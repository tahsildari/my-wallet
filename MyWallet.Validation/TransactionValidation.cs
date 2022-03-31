using FluentValidation;
using MyWallet.Models;

namespace MyWallet.Validation
{
    public class TransactionValidation : AbstractValidator<TransactionModel>
    {
        public TransactionValidation()
        {
            RuleFor(t => t.Account)
                .NotEmpty().WithMessage(Constants.AccountRequired);

            RuleFor(t => t.Amount)
                .NotNull().WithMessage(Constants.AmountRequired);

            RuleFor(t => t.Direction)
                .GreaterThanOrEqualTo((byte)TransactionDirection.Credit)
                    .WithMessage(Constants.DirectionInvalid)
                .LessThanOrEqualTo((byte)TransactionDirection.Debit)
                    .WithMessage(Constants.DirectionInvalid);

            RuleFor(t => t.Id)
                .GreaterThanOrEqualTo(Constants.LowestAcceptableId)
                    .WithMessage(Constants.DirectionInvalid);

            RuleFor(t => t.Amount)
                .GreaterThanOrEqualTo(Constants.MinimumTransactionAmount)
                    .WithMessage(Constants.AmountTooLow);

        }
    }
}
