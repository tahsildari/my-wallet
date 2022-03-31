using FluentValidation.Results;
using System;

namespace MyWallet.Validation
{
    public static class Extensions
    {
        public static string ReadValidationErrors(this ValidationResult validation)
            => String.Join(" - ", validation.Errors);
    }
}
