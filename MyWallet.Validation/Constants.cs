using System;
using System.Collections.Generic;
using System.Text;

namespace MyWallet.Validation
{
    public static class Constants
    {
        public static readonly string AccountRequired = "Account is required";
        public static readonly string AmountRequired = "Amount is not valid";
        public static readonly string DirectionInvalid = "Direction is not valid";
        public static readonly string AmountTooLow = $"Minimum transaction amount is {MinimumTransactionAmount}";
               
        public static readonly int LowestAcceptableId = 1;
        public static readonly double MinimumTransactionAmount = 0.1D;
    }
}

