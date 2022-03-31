namespace MyWallet.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public byte Direction { get; set; }
        public int Account { get; set; }
    }
}
