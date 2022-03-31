namespace MyWallet.Models
{
    /// <summary>
    /// Indicates whether a transaction is adding to an account's balance
    /// or lowering the balance
    /// </summary>
    public enum TransactionDirection : byte
    {
        /// <summary>
        /// Money that is added to an Account
        /// </summary>
        Credit = 0,
        /// <summary>
        /// Money that is subtracted from an Account
        /// </summary>
        Debit
    }
}
