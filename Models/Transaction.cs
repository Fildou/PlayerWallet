namespace PlayerWallet.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid PlayerId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
    public enum TransactionType
    {
        Deposit,
        Stake,
        Win
    }
}
